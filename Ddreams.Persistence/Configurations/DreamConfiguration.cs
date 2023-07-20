using Dddreams.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ddreams.Persistence.Configurations;

public class DreamConfiguration : IEntityTypeConfiguration<Dream>
{
    public void Configure(EntityTypeBuilder<Dream> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Title).HasMaxLength(255);
        builder.Property(d => d.Description).HasMaxLength(2000);

        builder.HasOne(d => d.Author)
            .WithMany()
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasIndex(d => d.Title);

        //Causes conflicts in DB 
        // builder
        //     .HasMany(d => d.Likes)
        //     .WithOne();



        //Relationships
        //One to many
        //Many dreams to one dream account

        //builder.HasOne<DreamsAccount>()
    }
}