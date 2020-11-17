namespace Restaurant.Model.Clases
{
    public class Articulo
    {
        public Articulo()
        {
            Id = 0;
            Nombre = string.Empty;
            Descripcion = string.Empty;
            Precio = 0;
            IdEstadoArticulo = 0;
            IdTipoConsumo = 0;
            UrlImagen = "";
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }
        public int IdEstadoArticulo { get; set; }
        public int IdTipoConsumo { get; set; }
        public EstadoArticulo EstadoArticulo { get; set; }
        public TipoConsumo TipoConsumo { get; set; }
        public string UrlImagen { get; set; }
    }
}
