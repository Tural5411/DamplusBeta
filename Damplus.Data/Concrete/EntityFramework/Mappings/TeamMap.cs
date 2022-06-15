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
    public class TeamMap : IEntityTypeConfiguration<Teams>
    {
        public void Configure(EntityTypeBuilder<Teams> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t =>t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Fullname).HasMaxLength(70).IsRequired();
            builder.Property(t => t.Photo).HasMaxLength(300).IsRequired();
            builder.Property(t => t.Position).HasMaxLength(50);
            builder.Property(t => t.IsActive).IsRequired();
            builder.Property(t => t.IsDeleted).IsRequired();
            builder.Property(t => t.ModifiedByName).HasMaxLength(50).IsRequired();
            builder.Property(t => t.CreatedByName).HasMaxLength(50).IsRequired();
            builder.Property(t => t.CreatedDate).IsRequired();
            builder.Property(t => t.ModifiedDate).IsRequired();

            builder.ToTable("Team");
        }
    }
}
