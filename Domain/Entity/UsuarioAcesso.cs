namespace Domain.Entity
{
    public class UsuarioAcesso
    {

        public int Id { get; set; }
        public int ModuloId { get; set; }
        public int ModuloItemId { get; set; }
        public virtual Modulos Modulo { get; set; }
        public virtual ModuloItem ModuloItem { get; set; }



    }
}
