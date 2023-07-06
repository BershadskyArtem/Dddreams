using Dddreams.Domain.Common;
using Dddreams.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ddreams.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    private readonly IDomainEventDispatcher _dispatcher;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt, IDomainEventDispatcher dispatcher) : base(opt)
    {
        _dispatcher = dispatcher;
    }
    
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Dream> Dreams { get; set; }
    public DbSet<DreamsAccount> Users { get; set; }
    public DbSet<Like> Likes { get; set; }
}