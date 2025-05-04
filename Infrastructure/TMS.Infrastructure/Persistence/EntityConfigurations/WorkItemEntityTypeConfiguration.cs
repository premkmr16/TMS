using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Core.Entities;

namespace TMS.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// Configures the entity type mapping for the <see cref="WorkItem"/> entity.
/// </summary>
public class WorkItemEntityTypeConfiguration : IEntityTypeConfiguration<WorkItem>
{
    /// <summary>
    /// Configures the database schema for the <see cref="WorkItem"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<WorkItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Id).HasColumnType("varchar(26)").IsRequired();
        builder.Property(x => x.WorkItemId).ValueGeneratedOnAdd();
        builder.Property(x => x.MemberId).HasColumnType("varchar(26)").IsRequired();
        builder.Property(x => x.Title).HasColumnType("varchar(200)").IsRequired();
        builder.Property(x => x.Description).HasColumnType("text").IsRequired();
        builder.Property(x => x.AcceptanceCriteria).HasColumnType("text").IsRequired();
        builder.Property(x => x.Tags).HasColumnType("text");
        builder.Property(x => x.StoryPoints).HasColumnType("integer");
        builder.Property(x => x.StatusId).HasColumnType("varchar(26)").IsRequired();
        builder.Property(x => x.PriorityId).HasColumnType("varchar(26)").IsRequired();
        builder.Property(x => x.ParentWorkItemId).HasColumnType("varchar(26)").IsRequired();
        builder.Property(x => x.CategoryId).HasColumnType("varchar(26)").IsRequired();
        builder.Property(x => x.CreatedBy).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.CreatedOn).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.ModifiedBy).HasColumnType("varchar(50)");
        builder.Property(x => x.ModifiedOn).HasColumnType("timestamp with time zone");
    }
}