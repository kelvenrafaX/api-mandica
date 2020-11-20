namespace Repository.Migrations
{
    using Domain.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            //SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(Context context)
        {
            List<DefEstadoCivil> listaDefEstadoCivil = new List<DefEstadoCivil>
            {
                new DefEstadoCivil { Id = 1, Descricao = "Solteiro", EstadoCivil = 'S' },
                new DefEstadoCivil { Id = 2, Descricao = "Casadp", EstadoCivil = 'C' },
                new DefEstadoCivil { Id = 3, Descricao = "Divorciado", EstadoCivil = 'D' },
                new DefEstadoCivil { Id = 4, Descricao = "Viúvo", EstadoCivil = 'V' }
            };
            foreach (var defEstadoCivil in listaDefEstadoCivil)
            {
                context.DefEstadoCivil.AddOrUpdate(x => x.Id, defEstadoCivil);
            }

            List<DefSexo> listasexo = new List<DefSexo>
            {
                new DefSexo { Id = 1, Descricao = "MASCULINO", Sexo = "M" },
                new DefSexo { Id = 2, Descricao = "FEMININO", Sexo = "F" }
            };
            foreach (var sexo in listasexo)
            {
                context.DefSexo.AddOrUpdate(x => x.Id, sexo);
            }

            List<DefTipoEntrega> listaDefTipoEntrega = new List<DefTipoEntrega>
            {
                new DefTipoEntrega { TipoEntrega = 1, Descricao = "Na Loja" },
                new DefTipoEntrega { TipoEntrega = 2, Descricao = "Entrega" }
            };
            foreach (var defTipoEntrega in listaDefTipoEntrega)
            {
                context.DefTipoEntrega.AddOrUpdate(x => x.TipoEntrega, defTipoEntrega);
            }

            List<DefTipoPedido> listaDefTipoPedido = new List<DefTipoPedido>
            {
                new DefTipoPedido { TipoPedido = 1, Descricao = "Orçamento" },
                new DefTipoPedido { TipoPedido = 2, Descricao = "Pedido" }
            };
            foreach (var defTipoPedido in listaDefTipoPedido)
            {
                context.DefTipoPedido.AddOrUpdate(x => x.TipoPedido, defTipoPedido);
            }

            List<DefTipoPessoa> listaDefTipoPessoa = new List<DefTipoPessoa>
            {
                new DefTipoPessoa { Id = 1, Tipo = "pf", Descricao = "Pessoa Física" },
                new DefTipoPessoa { Id = 2, Tipo = "pj", Descricao = "Pessoa Jurídica" }
            };
            foreach (var defTipoPessoa in listaDefTipoPessoa)
            {
                context.DefTipoPessoa.AddOrUpdate(x => x.Id, defTipoPessoa);
            }

            List<Natureza> listaNatureza = new List<Natureza>
            {
                new Natureza
                {
                    Id = 1,
                    Descricao = "Dinheiro",
                    Ativa = true,
                    NaturezaParcelas = new List<NaturezaParcelas> {
                    new NaturezaParcelas { NaturezaId = 1, Parcela = 1, Tarifacao = 0, DiasVencimento = 0, Ativa = true }
                }
                },
                new Natureza
                {
                    Id = 2,
                    Descricao = "Débito",
                    Ativa = true,
                    NaturezaParcelas = new List<NaturezaParcelas> {
                    new NaturezaParcelas { NaturezaId = 2, Parcela = 1, Tarifacao = 0, DiasVencimento = 0, Ativa = true }
                }
                },
                new Natureza
                {
                    Id = 3,
                    Descricao = "Crédito",
                    Ativa = true,
                    NaturezaParcelas = new List<NaturezaParcelas> {
                        new NaturezaParcelas { NaturezaId = 3, Parcela = 1, Tarifacao = 0, DiasVencimento = 30, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 3, Parcela = 2, Tarifacao = 0, DiasVencimento = 60, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 3, Parcela = 3, Tarifacao = 0, DiasVencimento = 90, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 3, Parcela = 4, Tarifacao = 0, DiasVencimento = 120, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 3, Parcela = 5, Tarifacao = 0, DiasVencimento = 150, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 3, Parcela = 6, Tarifacao = 0, DiasVencimento = 180, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 3, Parcela = 7, Tarifacao = 0, DiasVencimento = 210, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 3, Parcela = 8, Tarifacao = 0, DiasVencimento = 240, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 3, Parcela = 9, Tarifacao = 0, DiasVencimento = 270, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 3, Parcela = 10, Tarifacao = 0, DiasVencimento = 300, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 3, Parcela = 11, Tarifacao = 0, DiasVencimento = 330, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 3, Parcela = 12, Tarifacao = 0, DiasVencimento = 360, Ativa = true }
                    }
                },
                new Natureza
                {
                    Id = 5,
                    Descricao = "Transferência Bancária",
                    Ativa = true,
                    NaturezaParcelas = new List<NaturezaParcelas> {
                        new NaturezaParcelas { NaturezaId = 5, Parcela = 1, Tarifacao = 0, DiasVencimento = 30, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 5, Parcela = 2, Tarifacao = 0, DiasVencimento = 60, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 5, Parcela = 3, Tarifacao = 0, DiasVencimento = 90, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 5, Parcela = 4, Tarifacao = 0, DiasVencimento = 120, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 5, Parcela = 5, Tarifacao = 0, DiasVencimento = 150, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 5, Parcela = 6, Tarifacao = 0, DiasVencimento = 180, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 5, Parcela = 7, Tarifacao = 0, DiasVencimento = 210, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 5, Parcela = 8, Tarifacao = 0, DiasVencimento = 240, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 5, Parcela = 9, Tarifacao = 0, DiasVencimento = 270, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 5, Parcela = 10, Tarifacao = 0, DiasVencimento = 300, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 5, Parcela = 11, Tarifacao = 0, DiasVencimento = 330, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 5, Parcela = 12, Tarifacao = 0, DiasVencimento = 360, Ativa = true }
                    }

                },
                new Natureza
                {
                    Id = 4,
                    Descricao = "Boleto",
                    Ativa = true,
                    NaturezaParcelas = new List<NaturezaParcelas> {
                        new NaturezaParcelas { NaturezaId = 4, Parcela = 1, Tarifacao = 0, DiasVencimento = 30, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 4, Parcela = 2, Tarifacao = 0, DiasVencimento = 60, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 4, Parcela = 3, Tarifacao = 0, DiasVencimento = 90, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 4, Parcela = 4, Tarifacao = 0, DiasVencimento = 120, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 4, Parcela = 5, Tarifacao = 0, DiasVencimento = 150, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 4, Parcela = 6, Tarifacao = 0, DiasVencimento = 180, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 4, Parcela = 7, Tarifacao = 0, DiasVencimento = 210, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 4, Parcela = 8, Tarifacao = 0, DiasVencimento = 240, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 4, Parcela = 9, Tarifacao = 0, DiasVencimento = 270, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 4, Parcela = 10, Tarifacao = 0, DiasVencimento = 300, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 4, Parcela = 11, Tarifacao = 0, DiasVencimento = 330, Ativa = true },
                        new NaturezaParcelas { NaturezaId = 4, Parcela = 12, Tarifacao = 0, DiasVencimento = 360, Ativa = true }
                    }
                }
            };

            foreach (var natureza in listaNatureza)
            {
                context.Natureza.AddOrUpdate(x => x.Id, natureza);
            }

            List<Bandeira> listaBandeira = new List<Bandeira>
            {
                new Bandeira { Id = 1, Descricao = "VISA", Ativa = true, DataCadastro = DateTime.Now },
                new Bandeira { Id = 2, Descricao = "MASTERCARD", Ativa = true, DataCadastro = DateTime.Now },
                new Bandeira { Id = 3, Descricao = "DINERS", Ativa = true, DataCadastro = DateTime.Now },
                new Bandeira { Id = 4, Descricao = "AMEX", Ativa = true, DataCadastro = DateTime.Now },
                new Bandeira { Id = 5, Descricao = "SOLLO", Ativa = true, DataCadastro = DateTime.Now },
                new Bandeira { Id = 6, Descricao = "SIDECARD", Ativa = true, DataCadastro = DateTime.Now },
                new Bandeira { Id = 7, Descricao = "PRIVATE LABEL", Ativa = true, DataCadastro = DateTime.Now },
                new Bandeira { Id = 8, Descricao = "REDESHOP", Ativa = true, DataCadastro = DateTime.Now },
                new Bandeira { Id = 9, Descricao = "FININVEST", Ativa = true, DataCadastro = DateTime.Now },
                new Bandeira { Id = 10, Descricao = "JCB", Ativa = true, DataCadastro = DateTime.Now },
                new Bandeira { Id = 11, Descricao = "HIPERCARD", Ativa = true, DataCadastro = DateTime.Now },
                new Bandeira { Id = 12, Descricao = "LOSANGO", Ativa = true, DataCadastro = DateTime.Now },
                new Bandeira { Id = 13, Descricao = "SOROCRED", Ativa = true, DataCadastro = DateTime.Now },
                new Bandeira { Id = 14, Descricao = "DISCOVERY", Ativa = true, DataCadastro = DateTime.Now }
            };

            foreach (var bandeira in listaBandeira)
            {
                context.Bandeira.AddOrUpdate(x => x.Id, bandeira);
            }

            List<Categoria> listaCategoria = new List<Categoria>
            {
                new Categoria { Id = 1, Descricao = "ACERVO", Ativa = true, CategoriaPaiId = 0 },
                new Categoria { Id = 2, Descricao = "PRODUTO", Ativa = true, CategoriaPaiId = 0 },
                new Categoria { Id = 3, Descricao = "SERVIÇO", Ativa = true, CategoriaPaiId = 0 }
            };

            foreach (var categoria in listaCategoria)
            {
                context.Categoria.AddOrUpdate(x => x.Descricao, categoria);
            }

            List<Modulos> listaModulos = new List<Modulos>
            {
                new Modulos { Id = 1, Descricao = "RETAGUARDA", ModulosItem = new List<ModuloItem> {
                                                                                    new ModuloItem { Descricao="Cliente"},
                                                                                    new ModuloItem { Descricao="Funcionário"},
                                                                                    new ModuloItem { Descricao="Fornecedor"},
                                                                                    new ModuloItem { Descricao="Cargos"},
                                                                                    new ModuloItem { Descricao="Categorais"},
                                                                                    new ModuloItem { Descricao="Acervo"},
                                                                                    new ModuloItem { Descricao="Produto"},
                                                                                    new ModuloItem { Descricao="Serviço"},
                                                                                    new ModuloItem { Descricao="Forma de Pagamento"},
                                                                                    new ModuloItem { Descricao="Estoque"},
                                                                                    new ModuloItem { Descricao="Compras"}

                        }
                },
                new Modulos { Id = 2, Descricao = "COMERCIAL", ModulosItem =  new List<ModuloItem>{
                                                                                     new ModuloItem{Descricao="Pedidos" }

                    }
                },
                new Modulos { Id = 3, Descricao = "FINANCEIRO" }
            };

            foreach (var modulo in listaModulos)
            {
                context.Modulos.AddOrUpdate(x => x.Descricao, modulo);
            }

            //List<ModuloItem> listamoduloItems = new List<ModuloItem>();
            //listamoduloItems.Add(new ModuloItem { Descricao = "" });

        }


    }
}
