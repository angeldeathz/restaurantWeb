using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Model.Clases
{
    public class Reporte
    {
        public const int reporteDiario = 1;
        public const int reporteMensual = 2;

        public int IdReporte { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
    }
}
