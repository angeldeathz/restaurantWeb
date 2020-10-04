using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class EstadoOrdenService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/estadoOrdenes/";

        public EstadoOrdenService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<EstadoOrden> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<EstadoOrden>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public EstadoOrden Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<EstadoOrden>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(EstadoOrden estadoOrden)
        {
            var respuesta = _restClientHttp.Post<int>(_url, estadoOrden);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(EstadoOrden estadoOrden, int idEstadoOrden)
        {
            _url = $"{_url}{idEstadoOrden}";
            var respuesta = _restClientHttp.Put<bool>(_url, estadoOrden);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
