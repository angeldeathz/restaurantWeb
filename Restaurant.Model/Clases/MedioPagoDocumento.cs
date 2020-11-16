namespace Restaurant.Model.Clases
{
    public class MedioPagoDocumento
    {
        public MedioPagoDocumento()
        {
            Id = 0;
            Monto = 0;
            IdDocumentoPago = 0;
            IdMedioPago = 0;
        }

        public int Id { get; set; }
        public int Monto { get; set; }
        public int IdDocumentoPago { get; set; }
        public int IdMedioPago { get; set; }
        public DocumentoPago DocumentoPago { get; set; }
        public MedioPago MedioPago { get; set; }
    }
}
