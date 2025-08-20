using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Core.Entities;

namespace TMS.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// 
/// </summary>
public class EmployeeEducationEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeEducation>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Configure(EntityTypeBuilder<EmployeeEducation> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnType("varchar(26)").IsRequired();
        builder.Property(e => e.EmployeeId).HasColumnType("varchar(26)").IsRequired();
        builder.Property(e => e.EducationalLevel).HasColumnType("varchar(20)").IsRequired();
        builder.Property(e => e.BoardOrUniversity).HasColumnType("varchar(20)").IsRequired();
        builder.Property(e => e.Institution).HasColumnType("varchar(100)").IsRequired();
        builder.Property(e => e.PercentageOrCgpa).HasColumnType("numeric").IsRequired();
        builder.Property(e => e.YearOfPassing).HasColumnType("varchar(4)").IsRequired();
        builder.Property(e => e.Specialization).HasColumnType("varchar(20)");
        builder.Property(e => e.City).HasColumnType("varchar(50)").IsRequired();
        builder.Property(e => e.State).HasColumnType("varchar(50)").IsRequired();
        builder.Property(e => e.Country).HasColumnType("varchar(50)").IsRequired();
        builder.Property(e => e.PostalCode).HasColumnType("varchar(10)").IsRequired();
        builder.Property(x => x.CreatedBy).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.CreatedOn).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.ModifiedBy).HasColumnType("varchar(50)");
        builder.Property(x => x.ModifiedOn).HasColumnType("timestamp with time zone");
    }
}