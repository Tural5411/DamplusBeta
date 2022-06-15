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
    public class BusinessCategoryMap : IEntityTypeConfiguration<BusinessCategory>
    {
        public void Configure(EntityTypeBuilder<BusinessCategory> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.Property(b => b.Name).HasMaxLength(90).IsRequired();
            builder.Property(b => b.Icon).HasMaxLength(500).IsRequired();
            builder.Property(b => b.UpperCategoryId);
            builder.Property(b => b.Description).HasMaxLength(500);
            builder.Property(b => b.Link).HasMaxLength(500);
            builder.Property(b => b.IsActive).IsRequired();
            builder.Property(b => b.IsDeleted).IsRequired();
            builder.Property(b => b.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(b => b.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(b => b.CreatedDate).IsRequired();
            builder.Property(b => b.ModifiedDate).IsRequired();

            builder.ToTable("BusinessCategory");
        }
    }
}
