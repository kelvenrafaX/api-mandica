using Domain.Entity;
using MySql.Data.Entity;
using Repository.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class Context:DbContext
    {
        public Context()
            :base("ConexaoBancoMySQL")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>());
        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<DefEstadoCivil> DefEstadoCivil { get; set; }
        public DbSet<DefSexo> DefSexo { get; set; }
        public DbSet<DefTipoEntrega> DefTipoEntrega { get; set; }
        public DbSet<DefTipoPedido> DefTipoPedido { get; set; }
        public DbSet<DefTipoPessoa> DefTipoPessoa { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Orcamento> Orcamento { get; set; }
        public DbSet<OrcamentoProduto> OrcamentoProduto { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<ImagemProduto> ImagemProduto { get; set; }
        public DbSet<Imagem> Imagem { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<CompraProduto> CompraProduto { get; set; }
        public DbSet<Devolucao> Devolucao { get; set; }
        public DbSet<DevolucaoProduto> DevolucaoProduto { get; set; }
        public DbSet<Entrega> Entrega { get; set; }
        public DbSet<Loja> Loja { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<Natureza> Natureza { get; set; }
        public DbSet<NaturezaParcelas> NaturezaParcelas { get; set; }
        public DbSet<Bandeira> Bandeira { get; set; }
        public DbSet<Configuracao> Configuracoes { get; set; }

        public DbSet<Modulos> Modulos { get; set; }

        public DbSet<Cargo> Cargo { get; set; }

        public DbSet<ModuloItem> ModuloItem{ get; set; }

        public DbSet<UsuarioAcesso> UsuarioAcesso { get; set; }

        public DbSet<Fretes> Fretes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Properties<string>().Configure(p => { p.HasColumnType("varchar"); });
            modelBuilder.Properties<string>().Configure(p => { p.HasMaxLength(100); });

            modelBuilder.Entity<Imagem>()
            .Property(e => e.Descricao)
            .HasColumnType("text")
            .IsMaxLength();
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                if (entry.State == EntityState.Modified)
                    entry.Property("DataCadastro").IsModified = false;
            }
            return base.SaveChanges();
        }
    }
}
