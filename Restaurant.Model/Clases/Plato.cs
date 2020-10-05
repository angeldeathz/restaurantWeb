namespace Restaurant.Model.Clases
{
    public class Plato
    {
        public Plato()
        {
            Id = 0;
            Nombre = string.Empty;
            TiempoPreparacion = 0;
            IdArticulo = 0;
            IdTipoPreparacion = 0;
            Articulo = new Articulo();
            TipoPreparacion = new TipoPreparacion();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal TiempoPreparacion { get; set; }
        public int IdArticulo { get; set; }
        public int IdTipoPreparacion { get; set; }
        public Articulo Articulo { get; set; }
        public TipoPreparacion TipoPreparacion { get; set; }
    }
}
