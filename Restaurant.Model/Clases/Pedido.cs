﻿using System;

namespace Restaurant.Model.Clases
{
    public class Pedido
    {
        public Pedido()
        {
            Id = 0;
            FechaInicio = new DateTime();
            FechaTermino = new DateTime();
            Total = 0;
            IdEstadoPedido = 0;
            IdMesa = 0;
            Mesa = new Mesa();
        }

        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public int Total { get; set; }
        public int IdEstadoPedido { get; set; }
        public int IdMesa { get; set; }
        public Mesa Mesa { get; set; }
    }
}
