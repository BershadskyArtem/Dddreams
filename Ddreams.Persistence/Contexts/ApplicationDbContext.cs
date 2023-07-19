using Dddreams.Application.Common.Auth;
using Dddreams.Domain.Common;
using Dddreams.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ddreams.Persistence.Contexts;

public class ApplicationDbContext : IdentityDbContext<DreamsAccount,IdentityRole<Guid>,Guid>
{
    private readonly IDomainEventDispatcher _dispatcher;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt, IDomainEventDispatcher dispatcher) : base(opt)
    {
        _dispatcher = dispatcher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyMarker).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Dream> Dreams { get; set; }
    public DbSet<DreamsAccount> Users { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
}