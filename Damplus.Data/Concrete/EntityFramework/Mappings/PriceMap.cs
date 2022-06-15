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
    public class PriceMap : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.Property(b => b.Content).HasMaxLength(70);
            builder.Property(b => b.Header).HasMaxLength(300).IsRequired();
            builder.Property(b => b.Icon);
            builder.Property(b => b.PriceValue).IsRequired();
            builder.Property(b => b.Text);
            builder.Property(b => b.Text2);
            builder.Property(b => b.Text3);
            builder.Property(b => b.Text4);
            builder.Property(b => b.Text5);
            builder.Property(b => b.IsActive).IsRequired();
            builder.Property(b => b.IsDeleted).IsRequired();
            builder.Property(b => b.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(b => b.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(b => b.CreatedDate).IsRequired();
            builder.Property(b => b.ModifiedDate).IsRequired();

            builder.ToTable("Prices");
        }
    }
}
