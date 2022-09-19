using EstoqueLab.Domain.Entities;
using EstoqueLab.Uteis.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstoqueLab.Data.Mappings
{
    public class CategoriaMap : MapBase<Categoria>
    {
        public override void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");

            builder.HasKey(c => c.Id);

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