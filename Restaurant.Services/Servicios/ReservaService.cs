using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class ReservaService
    {
        private readonly RestClientHttp _restClientHttp;

        public ReservaService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<Reserva> Obtener()
        {
            string url = $"http://localhost/restaurant/api/reservas/";
            var respuesta = _restClientHttp.Get<List<Reserva>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Reserva Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/reservas/{id}";
            var respuesta = _restClientHttp.Get<Reserva>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(Reserva reserva)
        {
            string url = $"http://localhost/restaurant/api/reservas/";
            var respuesta = _restClientHttp.Post<int>(url, reserva);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(Reserva reserva, int idReserva)
        {
            string url = $"http://localhost/restaurant/api/reservas/{idReserva}";
            var respuesta = _restClientHttp.Put<bool>(url, reserva);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }

        public bool ModificarEstado(ReservaCambioEstado reservaCambioEstado)
        {
            string url = $"http://localhost/restaurant/api/reservas/NuevoEstado/";
            var respuesta = _restClientHttp.Post<bool>(url, reservaCambioEstado);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
