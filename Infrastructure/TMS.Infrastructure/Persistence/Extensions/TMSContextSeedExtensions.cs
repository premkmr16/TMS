// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using TMS.Core.Entities;
//
// namespace TMS.Infrastructure.Persistence.Extensions;
//
// /// <summary>
// /// 
// /// </summary>
// public static class TmsContextSeedExtensions
// {
//     /// <summary>
//     /// 
//     /// </summary>
//     /// <param name="builder"></param>
//     public static void SeedCategory(this EntityTypeBuilder<Category> builder)
//     {
//         builder.HasData(
//             new Category
//             {
//                 Id = Ulid,
//                 Type = "Story",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             },
//             new Category
//             {
//                 Id = string.NewUlid(),
//                 Type = "Bug",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             },
//             new Category
//             {
//                 Id = string.Newstring(),
//                 Type = "Spike",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             },
//             new Category
//             {
//                 Id = string.Newstring(),
//                 Type = "Defect",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             }
//         );
//     }
//
//     /// <summary>
//     /// 
//     /// </summary>
//     /// <param name="builder"></param>
//     public static void SeedPriority(this EntityTypeBuilder<Priority> builder)
//     {
//         builder.HasData(
//             new Priority
//             {
//                 Id = string.Newstring(),
//                 Type = "High",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             },
//             new Priority
//             {
//                 Id = string.Newstring(),
//                 Type = "Medium",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             },
//             new Priority
//             {
//                 Id = string.Newstring(),
//                 Type = "Low",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             }
//         );
//     }
//
//     /// <summary>
//     /// 
//     /// </summary>
//     /// <param name="builder"></param>
//     public static void SeedStatus(this EntityTypeBuilder<Status> builder)
//     {
//         builder.HasData(
//             new Status
//             {
//                 Id = string.Newstring(),
//                 Type = "In Progress",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             },
//             new Status
//             {
//                 Id = string.Newstring(),
//                 Type = "PR Submitted",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             },
//             new Status
//             {
//                 Id = string.Newstring(),
//                 Type = "Accepted",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             }
//         );
//     }
//
//     /// <summary>
//     /// 
//     /// </summary>
//     /// <param name="builder"></param>
//     public static void SeedEmployeeType(this EntityTypeBuilder<EmployeeType> builder)
//     {
//         builder.HasData(
//             new EmployeeType
//             {
//                 Id = string.Newstring(),
//                 Type = "Full Time",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             },
//             new EmployeeType
//             {
//                 Id = string.Newstring(),
//                 Type = "Contractor",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             },
//             new EmployeeType
//             {
//                 Id = string.Newstring(),
//                 Type = "Intern",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             },
//             new EmployeeType
//             {
//                 Id = string.Newstring(),
//                 Type = "Trainee",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             }
//         );
//     }
//
//     /// <summary>
//     /// 
//     /// </summary>
//     /// <param name="builder"></param>
//     public static void SeedProjectRole(this EntityTypeBuilder<ProjectRole> builder)
//     {
//         builder.HasData( 
//             new ProjectRole
//             {
//                 Id = string.Newstring(),
//                 Name = "Lead Engineer",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             },
//             new ProjectRole
//             {
//                 Id = string.Newstring(),
//                 Name = "Senior Engineer",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             },
//             new ProjectRole
//             {
//                 Id = string.Newstring(),
//                 Name = "Engineer",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             },
//             new ProjectRole
//             {
//                 Id = string.Newstring(),
//                 Name = "Architect",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             },
//             new ProjectRole
//             {
//                 Id = string.Newstring(),
//                 Name = "Project Manager",
//                 CreatedOn = DateTime.UtcNow,
//                 CreatedBy = "SYSTEM",
//                 ModifiedOn = DateTime.MinValue,
//                 ModifiedBy = string.Empty
//             });
//     }
// }