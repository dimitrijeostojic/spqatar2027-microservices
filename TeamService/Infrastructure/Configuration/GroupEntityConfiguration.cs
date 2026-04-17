using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

internal sealed class GroupEntityConfiguration
        : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Groups");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.GroupName)
            .IsRequired()
            .HasMaxLength(255);
    }
}
