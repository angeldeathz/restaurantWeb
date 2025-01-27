﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Model.Clases
{
    public class OrdenProveedor
    {
        public OrdenProveedor()
        {
            Id = 0;
            FechaHora = new DateTime();
            Total = 0;
            IdProveedor = 0;
            IdUsuario = 0;
            IdEstadoOrden = 0;
            EstadosOrdenProveedor = new List<EstadoOrden>();
        }

        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public int Total { get; set; }
        public int IdProveedor { get; set; }
        public int IdUsuario { get; set; }
        public Proveedor Proveedor { get; set; }
        public Usuario Usuario { get; set; }
        public int IdEstadoOrden { get; set; }
        public EstadoOrden EstadoOrden { get { return EstadosOrdenProveedor != null ? EstadosOrdenProveedor.LastOrDefault() : null; } }
        public List<EstadoOrden> EstadosOrdenProveedor { get; set; }
    }
}
