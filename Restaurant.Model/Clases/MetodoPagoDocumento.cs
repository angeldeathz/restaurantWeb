﻿namespace Restaurant.Model.Clases
{
    public class MetodoPagoDocumento
    {
        public int Id { get; set; }
        public int Monto { get; set; }
        public int IdDocumentoPago { get; set; }
        public int IdMetodoPago { get; set; }
        public DocumentoPago DocumentoPago { get; set; }
        public MetodoPago MetodoPago { get; set; }
    }
}
