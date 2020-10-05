namespace Restaurant.Model.Clases
{
    public class TipoUsuario
    {
        public TipoUsuario()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
