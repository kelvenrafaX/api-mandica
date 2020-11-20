using System.Collections.Generic;

namespace Domain.Function
{
    public class Paginador<Tipo> where Tipo : class
    {
        public List<Tipo> Dados { get; set; }
        public int Pagina { get; set; }
        public int ItensPorPagina { get; set; }
        public int QtdedePaginas { get; set; }
        public int QtdeItensTotal { get; set; }
    }
}
