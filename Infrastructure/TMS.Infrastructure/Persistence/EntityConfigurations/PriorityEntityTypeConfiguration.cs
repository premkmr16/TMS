using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Core.Entities;

namespace TMS.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// Configures the entity type mapping for the <see cref="Priority"/> entity.
/// </summary>
public class PriorityEntityTypeConfiguration : IEntityTypeConfiguration<Priority>
{
    /// <summary>
    /// Configures the database schema for the <see cref="Priority"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Priority> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Id).HasColumnType("varchar(26)").IsRequired();
        builder.Property(x => x.Type).HasColumnType("varchar(30)").IsRequired();
        builder.Property(x => x.CreatedBy).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.CreatedOn).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.ModifiedBy).HasColumnType("varchar(50)");
        builder.Property(x => x.ModifiedOn).HasColumnType("timestamp with time zone");
    }
}