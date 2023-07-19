using Dddreams.Application.Extensions;
using Dddreams.Domain.Extensions;
using Dddreams.Infrastructure.Auth;
using Dddreams.Infrastructure.Extensions;
using Dddreams.WebApi.Extensions;
using Dddreams.WebApi.Middlewares;
using Ddreams.Persistence.Contexts;
using Ddreams.Persistence.Extensions;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

//Configure Domain
builder.Services.AddDomain();
//Configure App
builder.Services.AddApplication();
//Configure Infrastracture 
builder.Services.AddInfrastructure(builder.Configuration);
//Configure Presistence
builder.Services.AddPresistence(builder.Configuration);

//Rest of the app
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.UseSwagger();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();


async Task AddRole(RoleManager<IdentityRole<Guid>> roleManager, string roleName)
{
    if (!await roleManager.RoleExistsAsync(roleName))
    {
        var role = new IdentityRole<Guid>(roleName);
        await roleManager.CreateAsync(role);
    }
}

using (var scope = app.Services.CreateScope())
{
    var container = scope.ServiceProvider;
    var db = container.GetService<ApplicationDbContext>();
    db.Database.EnsureCreated();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

    await AddRole(roleManager, Claims.Roles.Basic);
    await AddRole(roleManager, Claims.Roles.Paid);
    await AddRole(roleManager, Claims.Roles.Moderator);
    await AddRole(roleManager, Claims.Roles.Admin);
    await AddRole(roleManager, Claims.Roles.SuperAdmin);
}

app.Run();