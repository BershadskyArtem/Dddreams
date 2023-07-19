using Dddreams.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ddreams.Persistence.Configurations;

public class DreamsAccountConfiguration : IEntityTypeConfiguration<DreamsAccount>
{
    public void Configure(EntityTypeBuilder<DreamsAccount> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Description)
            .HasMaxLength(500);
        
        builder.HasMany(d => d.Dreams)
            .WithOne(d => d.Author);

        builder.HasMany(d => d.Comments)
            .WithOne(c => c.Author)
            .HasForeignKey(c => c.AuthorId);

        builder.HasMany(d => d.Likes)
            .WithOne(c => c.Author);

        builder.HasMany(d => d.Friends);


        builder.HasIndex(d => d.Email).IsUnique();
        builder.HasIndex(d => d.Nickname);
        

    }
}