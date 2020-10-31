namespace Restaurant.Model.Clases
{
    public class Plato
    {
        public Plato()
        {
            Id = 0;
            Nombre = string.Empty;
            MinutosPreparacion = 0;
            IdArticulo = 0;
            IdTipoPreparacion = 0;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal MinutosPreparacion { get; set; }
        public int IdArticulo { get; set; }
        public int IdTipoPreparacion { get; set; }
        public Articulo Articulo { get; set; }
        public TipoPreparacion TipoPreparacion { get; set; }
    }
}
