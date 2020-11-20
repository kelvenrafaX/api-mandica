using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Entity
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public Tipo Tipo { get; set; }

        public Situacao Situacao { get; set; }

        [MaxLength(200)]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public int CategoriaId { get; set; }

        public decimal ValorUnitarioLocacao { get; set; }

        public decimal ValorUnitarioReposicao { get; set; }

        public decimal ValorCustoProduto { get; set; }

        public int FornecedorId { get; set; }

        public virtual List<ImagemProduto> ImagemProdutos { get; set; }

        public int Inativo { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Terceiros { get; set; }

        public string Cor { get; set; }

        public decimal Altura { get; set; }

        public decimal Profundidade { get; set; }

        public decimal Largura { get; set; }

        public int EstoqueMin { get; set; }

        public int EstoqueMax { get; set; }

        [NotMapped]
        public int Quantidade { get; set; }

        [NotMapped]
        public ICollection<EstoqueComprometido> EstoqueComprometido { get; set; }

        // Relacionamentos
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual Categoria Categoria { get; set; }

        public ImagemProduto ImagemPrincipal { 
            get
            {
                return ImagemProdutos.Where(x => x.Imagem.Principal == true).Count() > 0 ? ImagemProdutos.Where(x => x.Imagem.Principal == true).First() : null;
            } 
        }
       
    }

    public enum Tipo
    {
        ACERVO,
        PRODUTO,
        SERVICO,
        TODOS = -1
    }

    public enum Situacao
    {
        PERFEITOESTADO = 0,
        LEVEDEFEITO = 1,
        AVARIADO = 2,
        QUEBRADO = 3,
        TODOS = -1
    }
}
