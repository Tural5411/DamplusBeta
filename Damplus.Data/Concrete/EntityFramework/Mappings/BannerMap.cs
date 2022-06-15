using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Damplus.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Data.Concrete.EntityFramework.Mappings
{
    public class BannerMap : IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.Property(b => b.Title).HasMaxLength(70).IsRequired();
            builder.Property(b => b.Thumbnail).HasMaxLength(300).IsRequired();
            builder.Property(b => b.IsMain).HasColumnType("bit");
            builder.Property(b => b.Description).HasMaxLength(500);
            builder.Property(b => b.IsActive).IsRequired();
            builder.Property(b => b.IsDeleted).IsRequired();
            builder.Property(b => b.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(b => b.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(b => b.CreatedDate).IsRequired();
            builder.Property(b => b.ModifiedDate).IsRequired();

            builder.ToTable("Banners");
        }
    }
}
