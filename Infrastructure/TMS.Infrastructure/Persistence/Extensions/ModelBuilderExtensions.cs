using Microsoft.EntityFrameworkCore;

namespace TMS.Infrastructure.Persistence.Extensions;

/// <summary>
/// 
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// Configures global cascade delete behavior for entity relationships.
    /// This ensures that related entities are automatically deleted when a parent entity is removed.
    /// </summary>
    /// <param name="modelBuilder">The <see cref="ModelBuilder"/> used to define entity relationships and constraints.</param>
    public static void ConfigureCascadeDelete(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var foreignKey in entityType.GetForeignKeys())
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
            }
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public static void ConfigureUlidForInsert(this DbContext context)
    {
        foreach (var entry in context.ChangeTracker.Entries())
        {
            var entityType = entry.Entity.GetType();
            var idProperty = entityType.GetProperty("Id");

            if (entry.State == EntityState.Added && idProperty != null && idProperty.PropertyType == typeof(string))
                idProperty.SetValue(entry.Entity, Ulid.NewUlid().ToString());
        }   
    }

    /// <summary>
    /// Seeds the database with initial data when the model is created.
    /// This method pre-populates tables with default values.
    /// </summary>
    /// <param name="modelBuilder"></param>
    // public static void SetupInitialEntities(this ModelBuilder modelBuilder) 
    // {
    //     modelBuilder.Entity<Category>().SeedCategory();
    //     modelBuilder.Entity<Priority>().SeedPriority();
    //     modelBuilder.Entity<Status>().SeedStatus();
    //     modelBuilder.Entity<EmployeeType>().SeedEmployeeType();
    //     modelBuilder.Entity<ProjectRole>().SeedProjectRole();
    // }
}