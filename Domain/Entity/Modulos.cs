using System.Collections.Generic;

namespace Domain.Entity
{
    public class Modulos
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<ModuloItem> ModulosItem { get; set; }

    }
}
