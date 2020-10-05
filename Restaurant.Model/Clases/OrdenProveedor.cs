using System;

namespace Restaurant.Model.Clases
{
    public class OrdenProveedor
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public int Total { get; set; }
        public int IdProveedor { get; set; }
        public int IdUsuario { get; set; }
        public Proveedor Proveedor { get; set; }
        public Usuario Usuario { get; set; }
        public int IdEstadoOrden { get; set; }
        public EstadoOrden EstadoOrden { get; set; }
    }
}
