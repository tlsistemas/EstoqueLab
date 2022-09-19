using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueLab.Uteis.Bases
{
    public class MapBase<T> : IEntityTypeConfiguration<T> 
        where T : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .IsRequired()
                .HasColumnName("Id");
            builder.Property(c => c.Ativo)
                .IsRequired()
                .HasColumnName("Ativo").HasDefaultValue(true);

            builder.Property(c => c.CriadoEm)
                .IsRequired()
                .HasColumnName("CriadoEm").HasDefaultValue(DateTime.Now);

            builder.Property(c => c.AtualizadoEm)
                .HasColumnName("AtualizadoEm").HasDefaultValue(DateTime.Now);
        }
    }
}
