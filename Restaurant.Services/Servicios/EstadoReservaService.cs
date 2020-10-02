using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class EstadoReservaService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/estadoReservas/";

        public EstadoReservaService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<EstadoReserva> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<EstadoReserva>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public EstadoReserva Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<EstadoReserva>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(EstadoReserva estadoReserva)
        {
            var respuesta = _restClientHttp.Post<int>(_url, estadoReserva);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(EstadoReserva estadoReserva)
        {
            var respuesta = _restClientHttp.Put<bool>(_url, estadoReserva);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
