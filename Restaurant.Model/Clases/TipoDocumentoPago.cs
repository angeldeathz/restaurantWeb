namespace Restaurant.Model.Clases
{
    public class TipoDocumentoPago
    {
        public const int boleta = 1;
        public const int factura = 2;

        public TipoDocumentoPago()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
