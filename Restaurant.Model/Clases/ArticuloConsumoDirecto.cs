namespace Restaurant.Model.Clases
{
    public class ArticuloConsumoDirecto
    {
        public ArticuloConsumoDirecto()
        {
            Id = 0;
            IdArticulo = 0;
            IdInsumo = 0;
        }

        public int Id { get; set; }
        public int IdArticulo { get; set; }
        public int IdInsumo { get; set; }
        public Articulo Articulo { get; set; }
        public Insumo Insumo { get; set; }
    }
}
