namespace Restaurant.Model.Clases
{
    public class MetodoPagoDocumento
    {
        public MetodoPagoDocumento()
        {
            Id = 0;
            Monto = 0;
            IdDocumentoPago = 0;
            IdMetodoPago = 0;
        }

        public int Id { get; set; }
        public int Monto { get; set; }
        public int IdDocumentoPago { get; set; }
        public int IdMetodoPago { get; set; }
        public DocumentoPago DocumentoPago { get; set; }
        public MetodoPago MetodoPago { get; set; }
    }
}
