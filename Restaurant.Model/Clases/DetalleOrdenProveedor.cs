namespace Restaurant.Model.Clases
{
    public class DetalleOrdenProveedor
    {
        public DetalleOrdenProveedor()
        {
            Id = 0;
            Precio = 0;
            Cantidad = 0;
            Total = 0;
            IdInsumo = 0;
            IdOrdenProveedor = 0;
        }

        public int Id { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public int Total { get; set; }
        public int IdInsumo { get; set; }
        public int IdOrdenProveedor { get; set; }
        public Insumo Insumo { get; set; }
        public OrdenProveedor OrdenProveedor { get; set; }
    }
}
