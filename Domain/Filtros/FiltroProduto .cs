using Domain.Entity;

namespace Domain.Filtros
{
    public class FiltroProduto : FiltroBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Fornecedor { get; set; }
        public int Categoria { get; set; }
        public int Inativo { get; set; }
        public Situacao Situacao { get; set; }
        public bool Terceiros { get; set; }

    }
}
