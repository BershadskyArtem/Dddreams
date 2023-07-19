using Dddreams.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ddreams.Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(d => d.Id);
        
        builder.HasMany(c => c.Likes)
            .WithOne()
            .HasForeignKey(d => d.LikableId);

        builder.HasOne(c => c.Author)
            .WithMany()
            .HasForeignKey(c => c.AuthorId);

        builder.HasOne(d => d.Parent)
            .WithMany();
    }
}