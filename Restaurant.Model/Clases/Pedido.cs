using System;

namespace Restaurant.Model.Clases
{
    public class Pedido
    {
        public Pedido()
        {
            Id = 0;
            FechaHoraFin = new DateTime();
            FechaHoraFin = new DateTime();
            Total = 0;
            IdEstadoPedido = 0;
            IdReserva = 0;
        }
        public int Id { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public int Total { get; set; }
        public int IdEstadoPedido { get; set; }
        public int IdReserva { get; set; }
        public Reserva Reserva { get; set; }
        public EstadoPedido EstadoPedido { get; set; }
    }
}
