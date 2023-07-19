using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dddreams.Application.Common.Auth;
using Dddreams.Application.Features.User.Commands.Login;
using Dddreams.Application.Features.User.Commands.Refresh;
using Dddreams.Application.Features.User.Commands.Register;
using Dddreams.Application.Interfaces.Repositories;
using Dddreams.Domain.Entities;
using Dddreams.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Dddreams.Infrastructure.Auth;

public class UserAuthService : IUserAuthService
{
    private readonly UserManager<DreamsAccount> _userManager;
    private readonly AuthConfiguration _authConfiguration;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly IRefreshTokensRepository _refreshTokensRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserAuthService(UserManager<DreamsAccount> userManager, AuthConfiguration authConfiguration,
        IRefreshTokensRepository refreshTokensRepository, IUnitOfWork unitOfWork,
        TokenValidationParameters tokenValidationParameters)
    {
        _userManager = userManager;
        _authConfiguration = authConfiguration;
        _refreshTokensRepository = refreshTokensRepository;
        _unitOfWork = unitOfWork;
        _tokenValidationParameters = tokenValidationParameters;
    }

    public async Task<UserToken> RegisterUserAsync(RegisterAccountCommand request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            return UserToken.Fail("Email is already used.");

        var user = DreamsAccount.Create(request.FirstName, request.LastName, request.Nickname, request.Email,
            string.Empty, request.DateOfBirth, DreamsRole.Basic);

        var createdUser = await _userManager.CreateAsync(user,request.Password);

        if (!createdUser.Succeeded)
        {
            return UserToken.Fail(createdUser.Errors.Select(x => x.Description).ToList());
        }

        var role = new Claim(Claims.Roles.Signature, Claims.Roles.Basic);

        //Dirty shit needs to be in a env variables
        if (request.Email == _authConfiguration.SuperAdminEmail)
            role = new Claim(Claims.Roles.Signature, Claims.Roles.SuperAdmin);

        await _userManager.AddClaimsAsync(user, new List<Claim>()
        {
            role,
            new Claim(Claims.Comments.CanComment, "true"),
            new Claim(Claims.Dreams.CanPostADream, "true"),
            new Claim(Claims.Likes.CanLeaveALike, "true"),
        });

        await _unitOfWork.SaveChangesAsync();

        return await GetJwtTokenAsync(user);
    }

    public async Task<UserToken> LoginUserAsync(LoginUserCommand request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser == null)
            return UserToken.Fail("User does not exist.");

        var pss = request.Password;
        
        var userHasValidPassword = await _userManager.CheckPasswordAsync(existingUser, pss);

        if (!userHasValidPassword)
            return UserToken.Fail("Invalid credentials.");

        return await GetJwtTokenAsync(existingUser);
    }

    public async Task<UserToken> RefreshTokenAsync(RefreshTokenCommand request)
    {
        var tokenPrinciple = GetTokenPrinciple(request.Token);

        if (tokenPrinciple == null)
            return UserToken.Fail("Invalid token");
        var expiresInSecondFromBase = long.Parse(tokenPrinciple.Claims.GetClaimValue(JwtRegisteredClaimNames.Exp));
        var expiryUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        expiryUtc = expiryUtc.AddSeconds(expiresInSecondFromBase);
        
        if(expiryUtc > DateTime.UtcNow)
            return UserToken.Fail("Token has not expired yet.");

        var jwi = tokenPrinciple.Claims.GetClaimValue(JwtRegisteredClaimNames.Jti);
        var storedRefreshToken = await _refreshTokensRepository.GetByIdAsync(jwi);
        
        if(storedRefreshToken == null)
            return UserToken.Fail("No token for you stupid hacker.");

        if(DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            return UserToken.Fail("Refresh token has expired.");
        
        if(storedRefreshToken.Invalidated)
            return UserToken.Fail("Invalid token.");
        
        if(storedRefreshToken.Used)
            return UserToken.Fail("Invalid token.");
        
        if(storedRefreshToken.JwtId != jwi)
            return UserToken.Fail("Wrong token.");

        storedRefreshToken.Used = true;
        _refreshTokensRepository.Update(storedRefreshToken);
        await _unitOfWork.SaveChangesAsync();
        var user = await _userManager
            .FindByIdAsync(tokenPrinciple.Claims.GetClaimValue(Claims.Accounts.Id));
        
        if(user == null)
            return UserToken.Fail("User does not exist.");

        return await GetJwtTokenAsync(user);
    }
    
    private ClaimsPrincipal? GetTokenPrinciple(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        
        try
        {
            var principal = handler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
            return !IsValidJwtSecurityAlgorithm(validatedToken) ? null : principal;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    private bool IsValidJwtSecurityAlgorithm(SecurityToken token)
    {
        return (token is JwtSecurityToken jwtToken) &&
               jwtToken.Header.Alg.Equals("HS256", StringComparison.InvariantCultureIgnoreCase);
    }

    private async Task<UserToken> GetJwtTokenAsync(DreamsAccount user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_authConfiguration.Key);

        if (user.Email == null)
            throw new ApplicationException("Something went horribly wrong here!");
        

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(Claims.Accounts.Id, user.Id.ToString()),
        };


        var userClaims = await _userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddSeconds(_authConfiguration.TokenLifeTimeInSeconds),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var refreshToken = new RefreshToken
        {
            JwtId = token.Id,
            //User = user,
            CreationDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddDays(_authConfiguration.RefreshTokenLifeTimeInDays),
            Token = Guid.NewGuid().ToString()
        };

        await _refreshTokensRepository.AddAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync();
        return new UserToken()
        {
            IsSuccess = true,
            RefreshToken = refreshToken.Token,
            Token = tokenHandler.WriteToken(token)
        };
    }
}