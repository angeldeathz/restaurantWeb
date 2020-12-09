using System;
using System.Collections.Generic;
using System.Linq;

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
            EstadosReserva = new List<EstadoReserva>();
        }

        public int Id { get; set; }
        public DateTime FechaReserva { get; set; }
        public int CantidadComensales { get; set; }
        public int IdCliente { get; set; }
        public int IdMesa { get; set; }
        public int IdEstadoReserva { get; set; }
        public Cliente Cliente { get; set; }
        public Mesa Mesa { get; set; }
        public EstadoReserva EstadoReserva { get { return EstadosReserva != null ? EstadosReserva.LastOrDefault() : null; } }
        public List<EstadoReserva> EstadosReserva { get; set; }
    }
}
