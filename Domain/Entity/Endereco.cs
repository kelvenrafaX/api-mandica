using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        public string Cep { get; set; }
       
        public string Rua { get; set; }

        public int Numero { get; set; }

        public string Complemento { get; set; }
        
        public string Bairro { get; set; }
        
        public string Cidade { get; set; }

        public string CodigoIbge { get; set; }

       public string UF { get; set; }

        public TipoEndereco Tipo { get; set; }

    }

    public enum TipoEndereco
    {
        PESSOAL,
        ENTREGA
    }
}

