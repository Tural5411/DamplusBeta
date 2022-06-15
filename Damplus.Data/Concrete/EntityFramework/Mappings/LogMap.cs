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
    public class LogMap : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).ValueGeneratedOnAdd();

            builder.Property(l => l.MachineName).HasMaxLength(50).IsRequired();
            builder.Property(l => l.Logged).IsRequired();
            builder.Property(l => l.Level).HasMaxLength(50).IsRequired();
            builder.Property(l => l.Message).HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.Property(l => l.Logger).HasMaxLength(250).IsRequired(false);
            builder.Property(l => l.CallSite).HasColumnType("NVARCHAR(MAX)").IsRequired(false);
            builder.Property(l => l.Exception).HasColumnType("NVARCHAR(MAX)").IsRequired(false);

            builder.ToTable("Logs");
        }
    }
}
