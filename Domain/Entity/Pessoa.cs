using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Pessoa
    {
        [Key]
        public int Id  { get; set; }

        //[MaxLength(200)]
        public string Nome { get; set; }
        
        //[MaxLength(14)]
        public string Cpf { get; set; }

        public string Cnpj { get; set; }

        public string NomeContato { get; set; }

        //[MaxLength(20)]
        public string Rg { get; set; }

        public string SiglaSexo { get; set; }

        public string Pais { get; set; }

        //[MaxLength(15)]
        public string Telefone { get; set; }

        //[MaxLength(15)]
        public string Celular { get; set; }

        //[MaxLength(200)]
        //[Required(ErrorMessage = "Informe o Email")]
        public string Email { get; set; }

        //[MaxLength(200)]
        public string EmailHash { get; set; }

        public virtual ICollection<Endereco> Enderecos { get; set; }

        public char SiglaEstadoCivil { get; set; }

        public DateTime? DataNascimento { get; set; }

        //[MaxLength(45)]
        public string Profissao { get; set; }

        //[MaxLength(45)]
        public string Facebook { get; set; }

        //[MaxLength(45)]
        public string Instagram { get; set; }

        public decimal Desconto  { get; set; }

        //[MaxLength(100)]
        public string Obs { get; set; }

        public string TipoPessoa { get; set; }

        public string Tipo { get; set; }

        public int Inativo { get; set; }

        public DateTime DataCadastro { get; set; }

        // Relacionamentos
        public virtual DefEstadoCivil EstadoCivil { get; set; }
        public virtual DefSexo Sexo { get; set; }
    }
}
