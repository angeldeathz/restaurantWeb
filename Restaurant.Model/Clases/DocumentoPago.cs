﻿using System;

namespace Restaurant.Model.Clases
{
    public class DocumentoPago
    {
        public DocumentoPago()
        {
            Id = 0;
            FechaPago = new DateTime();
            Total = 0;
            IdPedido = 0;
            IdTipoDocumentoPago = 0;
            Pedido = new Pedido();
            TipoDocumentoPago =  new TipoDocumentoPago();
        }

        public int Id { get; set; }
        public DateTime FechaPago { get; set; }
        public int Total { get; set; }
        public int IdPedido { get; set; }
        public int IdTipoDocumentoPago { get; set; }
        public Pedido Pedido { get; set; }
        public TipoDocumentoPago TipoDocumentoPago { get; set; }
    }
}
