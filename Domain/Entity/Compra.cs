using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }

        public string NumDocumento { get; set; }

        public virtual ICollection<CompraProduto> CompraProduto { get; set; }

        public int? FornecedorId { get; set; }

        public DateTime DataCompra { get; set; }

        // Relacionamentos
        public virtual Fornecedor Fornecedor { get; set; }
    }
}
