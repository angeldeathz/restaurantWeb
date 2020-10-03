using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class EstadoArticuloService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/estadoArticulos/";

        public EstadoArticuloService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<EstadoArticulo> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<EstadoArticulo>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public EstadoArticulo Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<EstadoArticulo>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(EstadoArticulo estadoArticulo)
        {
            var respuesta = _restClientHttp.Post<int>(_url, estadoArticulo);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(EstadoArticulo estadoArticulo, int idEstadoArticulo)
        {
            _url = $"{_url}{idEstadoArticulo}";
            var respuesta = _restClientHttp.Put<bool>(_url, estadoArticulo);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
