using Microsoft.EntityFrameworkCore;
using TMS.Core.Entities;
using TMS.Infrastructure.Persistence.Extensions;

namespace TMS.Infrastructure.Persistence.Context;

/// <summary>
/// Database context for the TMS - Task Management System.
/// </summary>
public class TmsDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TmsDbContext"/> class.
    /// </summary>
    /// <param name="options">The database context options.</param>
    public TmsDbContext(DbContextOptions<TmsDbContext> options) : base(options) { }
    
    /// <summary>
    /// Gets or sets the <see cref="Categories"/> table in the database.
    /// </summary>
    public DbSet<Category> Categories { get; set; }
    
    /// <summary>
    /// Gets or sets the <see cref="EmployeeCertifications"/> table in the database.
    /// </summary>
    public DbSet<EmployeeCertification> EmployeeCertifications { get; set; }
    
    /// <summary>
    /// Gets or sets the <see cref="EmployeeEducations"/> table in the database.
    /// </summary>
    public DbSet<EmployeeEducation> EmployeeEducations { get; set; }
    
    /// <summary>
    /// Gets or sets the <see cref="Employees"/> table in the database.
    /// </summary>
    public DbSet<Employee> Employees { get; set; }
    
    /// <summary>
    /// Gets or sets the <see cref="EmployeesIdentity"/> table in the database.
    /// </summary>
    public DbSet<EmployeeIdentity> EmployeesIdentity { get; set; }
    
    /// <summary>
    /// Gets or sets the <see cref="EmployeeRelations"/> table in the database.
    /// </summary>
    public DbSet<EmployeeRelation> EmployeeRelations { get; set; }
    
    /// <summary>
    /// Gets or sets the <see cref="EmployeeRoles"/> table in the database.
    /// </summary>
    public DbSet<EmployeeRole> EmployeeRoles { get; set; }
    
    /// <summary>
    /// Gets or sets the <see cref="EmployeeTypes"/> table in the database.
    /// </summary>
    public DbSet<EmployeeType> EmployeeTypes { get; set; }
    
    /// <summary>
    /// Gets or sets the <see cref="Priorities"/> table in the database.
    /// </summary>
    public DbSet<Priority> Priorities { get; set; }
    
    /// <summary>
    /// Gets or sets the <see cref="Projects"/> table in the database.
    /// </summary>
    public DbSet<Project> Projects { get; set; }
    
    /// <summary>
    /// Gets or sets the <see cref="ProjectMembers"/> table in the database.
    /// </summary>
    public DbSet<ProjectMember> ProjectMembers { get; set; }
    
    /// <summary>
    /// Gets or sets the <see cref="ProjectRoles"/> table in the database.
    /// </summary>
    public DbSet<ProjectRole> ProjectRoles { get; set; }
    
    /// <summary>
    /// Gets or sets the <see cref="Status"/> table in the database.
    /// </summary>
    public DbSet<Status> Status { get; set; }
    
    /// <summary>
    /// Gets or sets the <see cref="WorkItems"/> table in the database.
    /// </summary>
    public DbSet<WorkItem> WorkItems { get; set; }
    
    /// <summary>
    /// Gets or sets the <see cref="WorkItemDiscussions"/> table in the database.
    /// </summary>
    public DbSet<WorkItemDiscussion> WorkItemDiscussions { get; set; }

    /// <summary>
    /// Configures entity mappings, applies Fluent API configurations, enforces cascade delete, and seeds initial data.
    /// </summary>
    /// <param name="modelBuilder">Configures entity properties, relationships, constraints, and seeding data.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TmsDbContext).Assembly);
        modelBuilder.ConfigureCascadeDelete();
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Overrides SaveChangesAsync to handle audit tracking and ULID assignment.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Number of rows affected.</returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var currentTime = DateTimeOffset.UtcNow;
        const string defaultUser = "SYSTEM";

        this.ConfigureUlidForInsert();

        var entities = ChangeTracker.Entries<ITrackableEntity>()
            .Where(x => x.State is EntityState.Added or EntityState.Modified);

        foreach (var entity in entities)
        {
            switch (entity.State)
            {
                case EntityState.Added:
                {
                    entity.Entity.CreatedOn = currentTime;
                    entity.Entity.CreatedBy = defaultUser;
                    
                    entity.Entity.ModifiedOn = currentTime;
                    entity.Entity.ModifiedBy = string.Empty;
                    
                    break;
                }

                case EntityState.Modified:
                {
                    entity.Entity.ModifiedBy = defaultUser;
                    entity.Entity.ModifiedOn = currentTime;
                    
                    break;
                }
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}