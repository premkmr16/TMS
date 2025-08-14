using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Core.Entities;

namespace TMS.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// 
/// </summary>
public class EmployeeRelationEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeRelation>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Configure(EntityTypeBuilder<EmployeeRelation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.MentorId).HasColumnType("varchar(26)").IsRequired();
        builder.Property(x => x.MenteeId).HasColumnType("varchar(26)").IsRequired();
        builder.Property(x => x.IsActive).HasColumnType("boolean").HasDefaultValue(true);
        builder.Property(x => x.StartDate).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.EndDate).HasColumnType("timestamp with time zone");
        builder.Property(e => e.Id).HasColumnType("varchar(26)").IsRequired();
        builder.Property(x => x.CreatedBy).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.CreatedOn).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.ModifiedBy).HasColumnType("varchar(50)");
        builder.Property(x => x.ModifiedOn).HasColumnType("timestamp with time zone");
    }
}