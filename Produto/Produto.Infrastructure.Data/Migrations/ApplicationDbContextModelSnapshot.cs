﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Produto.Infrastructure.Data.Contexts;

#nullable disable

namespace Produto.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Produto.Domain.Entities.TblProduto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DataAlterado")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_alterado");

                    b.Property<DateTime?>("DataCriado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("data_criado")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Descricao")
                        .HasColumnType("text")
                        .HasColumnName("descricao");

                    b.Property<string>("Titulo")
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("titulo");

                    b.Property<decimal?>("Valor")
                        .HasColumnType("numeric")
                        .HasColumnName("valor");

                    b.HasKey("Id")
                        .HasName("tbl_produto_pkey");

                    b.HasIndex(new[] { "Titulo", "Id" }, "id_titulo_unico")
                        .IsUnique();

                    b.ToTable("tbl_produto", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
