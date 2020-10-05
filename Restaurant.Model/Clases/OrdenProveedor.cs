using System;

namespace Restaurant.Model.Clases
{
    public class OrdenProveedor
    {
        public OrdenProveedor()
        {
            Id = 0;
            FechaOrden = new DateTime();
            Total = 0;
            IdProveedor = 0;
            IdUsuario = 0;
            Proveedor = new Proveedor();
            Usuario = new Usuario();
            IdEstadoOrden = 0;
            EstadoOrden = new EstadoOrden();
        }

        public int Id { get; set; }
        public DateTime FechaOrden { get; set; }
        public int Total { get; set; }
        public int IdProveedor { get; set; }
        public int IdUsuario { get; set; }
        public Proveedor Proveedor { get; set; }
        public Usuario Usuario { get; set; }
        public int IdEstadoOrden { get; set; }
        public EstadoOrden EstadoOrden { get; set; }
    }
}
