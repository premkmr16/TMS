using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Core.Entities;

namespace TMS.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// 
/// </summary>
public class EmployeeIdentityEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeIdentity>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Configure(EntityTypeBuilder<EmployeeIdentity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnType("varchar(26)").IsRequired();
        builder.Property(e => e.EmployeeId).HasColumnType("varchar(26)").IsRequired();
        builder.Property(e => e.Email).HasColumnType("varchar(50)").IsRequired();
        builder.Property(e => e.EmergencyContactName).HasColumnType("varchar(50)").IsRequired();
        builder.Property(e => e.EmergencyContactNumber).HasColumnType("varchar(10)").IsRequired();
        builder.Property(e => e.AadharNumber).HasColumnType("varchar(12)").IsRequired();
        builder.Property(e => e.PanNumber).HasColumnType("varchar(10)").IsRequired();
        builder.Property(e => e.PassportNumber).HasColumnType("varchar(12)");
        builder.Property(e => e.VoterId).HasColumnType("varchar(18)").IsRequired();
        builder.Property(e => e.BloodGroup).HasColumnType("varchar(5)").IsRequired();
        builder.Property(e => e.Gender).HasColumnType("varchar(10)").IsRequired();
        builder.Property(e => e.CurrentAddress).HasColumnType("varchar(255)").IsRequired();
        builder.Property(e => e.PermanentAddress).HasColumnType("varchar(255)");
        builder.Property(e => e.City).HasColumnType("varchar(50)").IsRequired();
        builder.Property(e => e.State).HasColumnType("varchar(50)").IsRequired();
        builder.Property(e => e.Country).HasColumnType("varchar(50)").IsRequired();
        builder.Property(e => e.PostalCode).HasColumnType("varchar(10)").IsRequired();
    }
}