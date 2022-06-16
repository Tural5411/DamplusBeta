using Damplus.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Data.Concrete.EntityFramework.Mappings
{
    public class ProjectMap : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name).HasMaxLength(70).IsRequired();
            builder.Property(p => p.Info).IsRequired();
            builder.Property(p => p.Location);
            builder.Property(p => p.Price).HasMaxLength(10).IsRequired();
            builder.Property(p => p.StartDate).HasMaxLength(50).IsRequired();
            builder.Property(p => p.EndDate).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Photo).HasMaxLength(300).IsRequired();
            builder.Property(p => p.Description).HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.IsDeleted).IsRequired();
            builder.Property(p => p.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(p => p.CreatedDate).IsRequired();
            builder.Property(p => p.ModifiedDate).IsRequired();

            builder.HasOne<ProjectCategory>(p => p.ProjectCategory).WithMany(p => p.Projects).HasForeignKey(p => p.ProjectCategoryId);

            builder.ToTable("Projects");
        }
    }
}
