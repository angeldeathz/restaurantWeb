using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class EstadoReservaService
    {
        private readonly RestClientHttp _restClientHttp;

        public EstadoReservaService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<EstadoReserva> Obtener()
        {
            string url = $"http://localhost/restaurant/api/estadoReservas/";
            var respuesta = _restClientHttp.Get<List<EstadoReserva>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public EstadoReserva Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/estadoReservas/{id}";
            var respuesta = _restClientHttp.Get<EstadoReserva>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(EstadoReserva estadoReserva)
        {
            string url = $"http://localhost/restaurant/api/estadoReservas/";
            var respuesta = _restClientHttp.Post<int>(url, estadoReserva);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(EstadoReserva estadoReserva, int idEstadoReserva)
        {
            string url = $"http://localhost/restaurant/api/estadoReservas/{idEstadoReserva}";
            var respuesta = _restClientHttp.Put<bool>(url, estadoReserva);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
