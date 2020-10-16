using System;

namespace Restaurant.Model.Clases
{
    public class Reserva
    {
        public Reserva()
        {
            Id = 0;
            FechaReserva = new DateTime();
            IdCliente = 0;
            IdMesa = 0;
            IdEstadoReserva = 0;
            CantidadComensales = 0;
        }

        public int Id { get; set; }
        public DateTime FechaReserva { get; set; }
        public int CantidadComensales { get; set; }
        public int IdCliente { get; set; }
        public int IdMesa { get; set; }
        public int IdEstadoReserva { get; set; }
        public Cliente Cliente { get; set; }
        public Mesa Mesa { get; set; }
    }
}
