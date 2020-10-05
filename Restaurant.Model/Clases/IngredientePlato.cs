namespace Restaurant.Model.Clases
{
    public class IngredientePlato
    {
        public IngredientePlato()
        {
            Id = 0;
            CantidadInsumo = 0;
            IdInsumo = 0;
            IdPlato = 0;
        }

        public int Id { get; set; }
        public int CantidadInsumo { get; set; }
        public int IdInsumo { get; set; }
        public int IdPlato { get; set; }
        public Insumo Insumo { get; set; }
        public Plato Plato { get; set; }
    }
}
