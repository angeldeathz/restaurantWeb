namespace Restaurant.Model.Clases
{
    public class DetalleOrdenProveedor
    {
        public int Id { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public int Total { get; set; }
        public int IdInsumo { get; set; }
        public Insumo Insumo { get; set; }
        public OrdenProveedor ordenProveedor { get; set; }
        public int IdOrdenProveedor { get; set; }
    }
}
