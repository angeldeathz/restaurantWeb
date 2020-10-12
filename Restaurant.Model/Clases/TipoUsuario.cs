namespace Restaurant.Model.Clases
{
    public class TipoUsuario
    {
        public const int administrador = 1;
        public const int caja = 2;
        public const int bodega = 3;
        public const int garzon = 4;
        public const int cocina = 5;

        public TipoUsuario()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
