using Fiap.Web.Alunos.Models;
using Microsoft.EntityFrameworkCore;
namespace Fiap.Web.Alunos.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<RepresentanteModel> Representantes { get; set; }
        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<LojaModel> Lojas { get; set; }
        public DbSet<PedidoModel> Pedidos { get; set; }
        public DbSet<FornecedorModel> Fornecedores { get; set; }
        public DbSet<PedidoProdutoModel> PedidoProdutos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepresentanteModel>(entity =>
            {
                entity.ToTable("Representantes");
                entity.HasKey(e => e.RepresentanteId);
                entity.Property(e => e.NomeRepresentante).IsRequired();
                entity.HasIndex(e => e.Cpf).IsUnique(); 
            });
            modelBuilder.Entity<ClienteModel>(entity =>
            {
                // Define o nome da tabela para 'Clientes'
                entity.ToTable("Clientes"); 
                entity.HasKey(e => e.ClienteId); 
                entity.Property(e => e.Nome).IsRequired(); 
                entity.Property(e => e.Email).IsRequired();
                // Especifica o tipo de dado para DataNascimento
                entity.Property(e => e.DataNascimento).HasColumnType("date");
                entity.Property(e => e.Observacao).HasMaxLength(500);
                // Configuração da relação com RepresentanteModel
                // Define a relação de um para um com RepresentanteModel
                entity.HasOne(e => e.Representante)
                    // Indica que um Representante pode ter muitos Clientes
                    .WithMany()
                    // Define a chave estrangeira
                    .HasForeignKey(e => e.RepresentanteId)
                    // Torna a chave estrangeira obrigatória
                    .IsRequired(); 
            });
            // Configuração para ProdutoModel
            modelBuilder.Entity<ProdutoModel>(entity =>
            {
                entity.ToTable("Produtos");
                entity.HasKey(p => p.ProdutoId);
                entity.Property(p => p.Nome).IsRequired();
                entity.Property(p => p.Descricao);
                entity.Property(p => p.Preco).HasColumnType("NUMBER(18,2)");
                // Relacionamento com FornecedorModel
                entity.HasOne(p => p.Fornecedor)
                      .WithMany(f => f.Produtos)
                      .HasForeignKey(p => p.FornecedorId);
            });
            // Configuração para LojaModel
            modelBuilder.Entity<LojaModel>(entity =>
            {
                entity.ToTable("Lojas");
                entity.HasKey(l => l.LojaId);
                entity.Property(l => l.Nome).IsRequired();
                entity.Property(l => l.Endereco);
                // Relacionamento com PedidoModel
                entity.HasMany(l => l.Pedidos)
                      .WithOne(p => p.Loja)
                      .HasForeignKey(p => p.LojaId);
            });
            // Configuração para PedidoModel
            modelBuilder.Entity<PedidoModel>(entity =>
            {
                entity.ToTable("Pedidos");
                entity.HasKey(p => p.PedidoId);
                entity.Property(p => p.DataPedido).HasColumnType("DATE");
                // Relacionamento com ClienteModel
                entity.HasOne(p => p.Cliente)
                      .WithMany()
                      .HasForeignKey(p => p.ClienteId);
                // Configuração de muitos para muitos: PedidoModel e ProdutoModel
                entity.HasMany(p => p.PedidoProdutos)
                      .WithOne(pp => pp.Pedido)
                      .HasForeignKey(pp => pp.PedidoId);
            });
            // Configuração para FornecedorModel
            modelBuilder.Entity<FornecedorModel>(entity =>
            {
                entity.ToTable("Fornecedores");
                entity.HasKey(f => f.FornecedorId);
                entity.Property(f => f.Nome).IsRequired();
            });
            // Configuração para PedidoProdutoModel (relacionamento muitos-para-muitos)
            modelBuilder.Entity<PedidoProdutoModel>(entity =>
            {
                entity.HasKey(pp => new { pp.PedidoId, pp.ProdutoId });
                entity.HasOne(pp => pp.Pedido)
                      .WithMany(p => p.PedidoProdutos)
                      .HasForeignKey(pp => pp.PedidoId);
                entity.HasOne(pp => pp.Produto)
                      .WithMany(p => p.PedidoProdutos)
                      .HasForeignKey(pp => pp.ProdutoId);
            });
        }
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        protected DatabaseContext()
        {
        }
    }
}