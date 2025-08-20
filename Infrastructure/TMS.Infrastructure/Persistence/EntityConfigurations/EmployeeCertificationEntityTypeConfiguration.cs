using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Core.Entities;

namespace TMS.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// 
/// </summary>
public class EmployeeCertificationEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeCertification>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Configure(EntityTypeBuilder<EmployeeCertification> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnType("varchar(26)").IsRequired();
        builder.Property(e => e.EmployeeId).HasColumnType("varchar(26)").IsRequired();
        builder.Property(e => e.CertificationName).HasColumnType("varchar(100)").IsRequired();
        builder.Property(e => e.IssuingOrganization).HasColumnType("varchar(100)").IsRequired();
        builder.Property(e => e.IssuedDate).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(e => e.ExpirationDate).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(e => e.CertificationId).HasColumnType("varchar(50)").IsRequired();
        builder.Property(e => e.CertificationUrl).HasColumnType("varchar(200)");
        builder.Property(e => e.IsActive).HasColumnType("boolean").HasDefaultValueSql("true");
        builder.Property(e => e.CreatedBy).HasColumnType("varchar(50)").IsRequired();
        builder.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(e => e.ModifiedBy).HasColumnType("varchar(50)");
        builder.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");
    }
}