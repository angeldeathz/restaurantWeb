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
        public const int reporteClientes = 3;
        public const int reportPlatos = 4;
        public const int reporteTiempos = 5;

        public int IdReporte { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }

        public static IDictionary<int, string> GetTiposReporte()
        {
            IDictionary<int, string> tipoReportes = new Dictionary<int, string>();
            tipoReportes.Add(reporteDiario, "Utilidad diaria");
            tipoReportes.Add(reporteMensual, "Utilidad mensual");
            tipoReportes.Add(reporteClientes, "Clientes atendidos");
            tipoReportes.Add(reportPlatos, "Platos consumidos");
            tipoReportes.Add(reporteTiempos, "Tiempos de atención");
            return tipoReportes;
        }
    }
}
