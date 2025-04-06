using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Core.Entities;

namespace TMS.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// Configures the entity type mapping for the <see cref="Project"/> entity.
/// </summary>
public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
{
    /// <summary>
    /// Configures the database schema for the <see cref="Project"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Id).HasColumnType("varchar(26)").HasConversion(v => v.ToString(), v => Ulid.Parse(v));
        builder.Property(x => x.Title).HasColumnType("varchar(100)").IsRequired();
        builder.Property(x => x.StartDate).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.EndDate).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.ActualStartDate).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.ActualEndDate).HasColumnType("timestamp with time zone").HasDefaultValue(DateTimeOffset.MinValue);
        builder.Property(x => x.IsActive).HasColumnType("boolean").HasDefaultValue(true);
        builder.Property(x => x.CreatedBy).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.CreatedOn).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.ModifiedBy).HasColumnType("varchar(50)");
        builder.Property(x => x.ModifiedOn).HasColumnType("timestamp with time zone");
    }
}