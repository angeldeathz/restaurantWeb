using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class ReporteService
    {
        private readonly RestClientHttp _restClientHttp;

        public ReporteService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public string Obtener(Reporte reporte)
        {
            string parametros = $"reporte.idReporte={reporte.IdReporte}&reporte.idUsuario={reporte.IdUsuario}&reporte.fechaDesde={reporte.FechaDesde:yyyy-MM-dd}&reporte.fechaHasta={reporte.FechaHasta:yyyy-MM-dd}";
            string url = $"http://localhost/restaurant/api/reportes?{parametros}";
            var respuesta = _restClientHttp.Get<string>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }
    }
}
