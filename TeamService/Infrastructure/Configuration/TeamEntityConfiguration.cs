using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

internal sealed class TeamEntityConfiguration
    : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {

        builder.ToTable("Teams");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.TeamName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(c => c.FlagIcon)
             .IsRequired(false)
             .HasMaxLength(255);

        builder.Property(c => c.GroupId)
            .IsRequired(false)
            .HasColumnType("int");

        builder.HasOne(t => t.Group)
            .WithMany(g => g.Teams)
            .HasForeignKey(t => t.GroupId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
