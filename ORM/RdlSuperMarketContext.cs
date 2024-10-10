using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RDLSuperMarket.ORM;

public partial class RdlsuperMarketContext : DbContext
{
    public RdlsuperMarketContext()
    {
    }

    public RdlsuperMarketContext(DbContextOptions<RdlsuperMarketContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCliente> TbClientes { get; set; }

    public virtual DbSet<TbEndereco> TbEnderecos { get; set; }

    public virtual DbSet<TbProduto> TbProdutos { get; set; }

    public virtual DbSet<TbUsuario> TbUsuario { get; set; }

    public virtual DbSet<TbVendum> TbVendum { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAB205_2\\SQLEXPRESS;Database=RDLSuperMarket;User Id=RDLSuperMarket;Password=RDL271125;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbCliente>(entity =>
        {
            entity.ToTable("Tb_Cliente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Documentoid).HasColumnName("documentoid");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone).HasColumnName("telefone");
        });

        modelBuilder.Entity<TbEndereco>(entity =>
        {
            entity.ToTable("Tb_Endereco");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cep).HasColumnName("cep");
            entity.Property(e => e.Cidade)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cidade");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Fkcliente).HasColumnName("fkcliente");
            entity.Property(e => e.Logradouro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("logradouro");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.PontoReferencia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ponto_referencia");

            entity.HasOne(d => d.FkclienteNavigation).WithMany(p => p.TbEnderecos)
                .HasForeignKey(d => d.Fkcliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_Endereco_Tb_Cliente");
        });

        modelBuilder.Entity<TbProduto>(entity =>
        {
            entity.ToTable("Tb_Produto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Notaff).HasColumnName("notaff");
            entity.Property(e => e.Preco)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("preco");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.ToTable("Tb_Usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Senha)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("senha");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<TbVendum>(entity =>
        {
            entity.ToTable("Tb_Venda");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fkcliente).HasColumnName("fkcliente");
            entity.Property(e => e.Fkproduto).HasColumnName("fkproduto");
            entity.Property(e => e.Notafv).HasColumnName("notafv");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("valor");

            entity.HasOne(d => d.FkclienteNavigation).WithMany(p => p.TbVenda)
                .HasForeignKey(d => d.Fkcliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_Venda_Tb_Cliente");

            entity.HasOne(d => d.FkprodutoNavigation).WithMany(p => p.TbVenda)
                .HasForeignKey(d => d.Fkproduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tb_Venda_Tb_Produto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
