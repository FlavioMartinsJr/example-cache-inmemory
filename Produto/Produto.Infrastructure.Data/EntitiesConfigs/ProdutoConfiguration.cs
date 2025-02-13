using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Produto.Domain.Entities;

namespace Produto.Infrastructure.Data.EntitiesConfigs
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<TblProduto>
    {
        public void Configure(EntityTypeBuilder<TblProduto> builder)
        {
            builder.ToTable("tbl_produto");
            builder.HasKey(e => e.Id).HasName("tbl_produto_pkey");
            builder.Property(e => e.Id)
                .HasColumnName("id");
            builder.Property(e => e.Titulo)
                .HasMaxLength(150)
                .HasColumnName("titulo");
            builder.Property(e => e.Valor)
               .HasColumnName("valor");
            builder.Property(e => e.Descricao)
                .HasColumnName("descricao");
            builder.HasIndex(e => new { e.Titulo, e.Id }, "id_titulo_unico")
                .IsUnique();
            builder.Property(e => e.DataAlterado)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_alterado");
            builder.Property(e => e.DataCriado)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_criado");
            
        }
    }
}