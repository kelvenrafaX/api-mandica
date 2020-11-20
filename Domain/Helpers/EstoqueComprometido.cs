using System;

namespace Domain.Helpers
{
    public class EstoqueComprometido
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
