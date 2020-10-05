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
            IdMesa = 0;
            Mesa = new Mesa();
        }

        public int Id { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public int Total { get; set; }
        public int IdEstadoPedido { get; set; }
        public int IdMesa { get; set; }
        public Mesa Mesa { get; set; }
        public EstadoPedido EstadoPedido { get; set; }
    }
}
