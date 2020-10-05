namespace Restaurant.Model.Clases
{
    public class Insumo
    {
        public Insumo()
        {
            Id = 0;
            Nombre = string.Empty;
            StockActual = 0;
            StockOptimo = 0;
            StockCritico = 0;
            IdProveedor = 0;
            IdUnidadDeMedida = 0;
            Proveedor = new Proveedor();
            UnidadMedida = new UnidadMedida();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int StockActual { get; set; }
        public int StockOptimo { get; set; }
        public int StockCritico { get; set; }
        public int IdProveedor { get; set; }
        public int IdUnidadDeMedida { get; set; }
        public Proveedor Proveedor { get; set; }
        public UnidadMedida UnidadMedida { get; set; }
    }
}