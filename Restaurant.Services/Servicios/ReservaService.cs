using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class ReservaService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/reservas/";

        public ReservaService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<Reserva> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<Reserva>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Reserva Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<Reserva>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(Reserva reserva)
        {
            var respuesta = _restClientHttp.Post<int>(_url, reserva);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(Reserva reserva)
        {
            var respuesta = _restClientHttp.Put<bool>(_url, reserva);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
