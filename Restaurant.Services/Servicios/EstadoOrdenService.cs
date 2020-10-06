using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class EstadoOrdenService
    {
        private readonly RestClientHttp _restClientHttp;

        public EstadoOrdenService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<EstadoOrden> Obtener()
        {
            string url = $"http://localhost/restaurant/api/estadoOrdenesProveedor/";
            var respuesta = _restClientHttp.Get<List<EstadoOrden>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public EstadoOrden Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/estadoOrdenesProveedor/{id}";
            var respuesta = _restClientHttp.Get<EstadoOrden>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(EstadoOrden estadoOrden)
        {
            string url = $"http://localhost/restaurant/api/estadoOrdenesProveedor/";
            var respuesta = _restClientHttp.Post<int>(url, estadoOrden);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(EstadoOrden estadoOrden, int idEstadoOrden)
        {
            string url = $"http://localhost/restaurant/api/estadoOrdenesProveedor/{idEstadoOrden}";
            var respuesta = _restClientHttp.Put<bool>(url, estadoOrden);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
