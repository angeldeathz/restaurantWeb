using System;

namespace Restaurant.Model.Clases
{
    public class Pedido
    {
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
