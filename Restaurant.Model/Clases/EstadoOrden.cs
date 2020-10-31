using System;

namespace Restaurant.Model.Clases
{
    public class EstadoOrden
    {

        public EstadoOrden()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
    }
}
