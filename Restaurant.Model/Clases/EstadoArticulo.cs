namespace Restaurant.Model.Clases
{
    public class EstadoArticulo
    {
        public const int disponible = 1;
        public const int agotado = 2;
        public const int inactivo = 3;
        public EstadoArticulo()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
