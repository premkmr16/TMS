using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Core.Entities;

namespace TMS.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// Configures the entity type mapping for the <see cref="ProjectMember"/> entity.
/// </summary>
public class ProjectMemberEntityTypeConfiguration : IEntityTypeConfiguration<ProjectMember>
{
    /// <summary>
    /// Configures the database schema for the <see cref="ProjectMember"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<ProjectMember> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Id).HasColumnType("varchar(26)").HasConversion(v => v.ToString(), v => Ulid.Parse(v));
        builder.Property(x => x.ProjectId).HasColumnType("varchar(26)").IsRequired();
        builder.Property(x => x.EmployeeId).HasColumnType("varchar(26)").IsRequired();
        builder.Property(x => x.RoleId).IsRequired();
        builder.Property(x => x.Email).HasColumnType("varchar(100)").IsRequired();
        builder.Property(x => x.StartDate).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.EndDate).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.IsActive).HasColumnType("boolean").HasDefaultValue(true);
        builder.Property(x => x.CreatedBy).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.CreatedOn).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.ModifiedBy).HasColumnType("varchar(50)");
        builder.Property(x => x.ModifiedOn).HasColumnType("timestamp with time zone");
    }
}