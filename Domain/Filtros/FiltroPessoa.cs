namespace Domain.Filtros
{
    public class FiltroPessoa : FiltroBase
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Cnpj { get; set; }

        public string Rg { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public int Inativo { get; set; }
    }
}
