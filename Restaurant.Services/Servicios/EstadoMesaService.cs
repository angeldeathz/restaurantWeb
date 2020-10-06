using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class EstadoMesaService
    {
        private readonly RestClientHttp _restClientHttp;

        public EstadoMesaService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<EstadoMesa> Obtener()
        {
            string url = $"http://localhost/restaurant/api/estadoMesas/";
            var respuesta = _restClientHttp.Get<List<EstadoMesa>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public EstadoMesa Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/estadoMesas/{id}";
            var respuesta = _restClientHttp.Get<EstadoMesa>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(EstadoMesa estadoMesa)
        {
            string url = $"http://localhost/restaurant/api/estadoMesas/";
            var respuesta = _restClientHttp.Post<int>(url, estadoMesa);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(EstadoMesa estadoMesa, int idEstadoMesa)
        {
            string url = $"http://localhost/restaurant/api/estadoMesas/{idEstadoMesa}";
            var respuesta = _restClientHttp.Put<bool>(url, estadoMesa);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
