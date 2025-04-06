using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Core.Entities;

namespace TMS.Infrastructure.Persistence.Extensions;

/// <summary>
/// 
/// </summary>
public static class TmsContextSeedExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    public static void SeedCategory(this EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            new Category
            {
                Id = Ulid.NewUlid(),
                Type = "Story",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            },
            new Category
            {
                Id = Ulid.NewUlid(),
                Type = "Bug",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            },
            new Category
            {
                Id = Ulid.NewUlid(),
                Type = "Spike",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            },
            new Category
            {
                Id = Ulid.NewUlid(),
                Type = "Defect",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            }
        );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    public static void SeedPriority(this EntityTypeBuilder<Priority> builder)
    {
        builder.HasData(
            new Priority
            {
                Id = Ulid.NewUlid(),
                Type = "High",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            },
            new Priority
            {
                Id = Ulid.NewUlid(),
                Type = "Medium",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            },
            new Priority
            {
                Id = Ulid.NewUlid(),
                Type = "Low",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            }
        );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    public static void SeedStatus(this EntityTypeBuilder<Status> builder)
    {
        builder.HasData(
            new Status
            {
                Id = Ulid.NewUlid(),
                Type = "In Progress",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            },
            new Status
            {
                Id = Ulid.NewUlid(),
                Type = "PR Submitted",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            },
            new Status
            {
                Id = Ulid.NewUlid(),
                Type = "Accepted",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            }
        );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    public static void SeedEmployeeType(this EntityTypeBuilder<EmployeeType> builder)
    {
        builder.HasData(
            new EmployeeType
            {
                Id = Ulid.NewUlid(),
                Type = "Full Time",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            },
            new EmployeeType
            {
                Id = Ulid.NewUlid(),
                Type = "Contractor",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            },
            new EmployeeType
            {
                Id = Ulid.NewUlid(),
                Type = "Intern",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            },
            new EmployeeType
            {
                Id = Ulid.NewUlid(),
                Type = "Trainee",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            }
        );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    public static void SeedProjectRole(this EntityTypeBuilder<ProjectRole> builder)
    {
        builder.HasData( 
            new ProjectRole
            {
                Id = Ulid.NewUlid(),
                Name = "Lead Engineer",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            },
            new ProjectRole
            {
                Id = Ulid.NewUlid(),
                Name = "Senior Engineer",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            },
            new ProjectRole
            {
                Id = Ulid.NewUlid(),
                Name = "Engineer",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            },
            new ProjectRole
            {
                Id = Ulid.NewUlid(),
                Name = "Architect",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            },
            new ProjectRole
            {
                Id = Ulid.NewUlid(),
                Name = "Project Manager",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                ModifiedOn = DateTime.MinValue,
                ModifiedBy = string.Empty
            });
    }
}