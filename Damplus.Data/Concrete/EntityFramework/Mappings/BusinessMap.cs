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
    public class BusinessMap : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.Property(b => b.Title).HasMaxLength(70).IsRequired();
            builder.Property(b => b.Content).HasMaxLength(300).IsRequired();
            builder.Property(b => b.SeoAuthor);
            builder.Property(b => b.PID);
            builder.Property(b => b.SeoTags);
            builder.Property(b => b.YoutubeLink);
            builder.Property(b => b.ThumbNail).HasMaxLength(500);
            builder.Property(b => b.SeoDescription).HasMaxLength(500);
            builder.Property(b => b.FileName);
            builder.Property(b => b.Link).HasMaxLength(500);
            builder.Property(b => b.IsActive).IsRequired();
            builder.Property(b => b.IsDeleted).IsRequired();
            builder.Property(b => b.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(b => b.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(b => b.CreatedDate).IsRequired();
            builder.Property(b => b.ModifiedDate).IsRequired();

            builder.ToTable("Businesses");
        }
    }
}
