using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class HorarioReservaService
    {
        private readonly RestClientHttp _restClientHttp;

        public HorarioReservaService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<HorarioReserva> Obtener()
        {
            string url = $"http://localhost/restaurant/api/horarioReservas/";
            var respuesta = _restClientHttp.Get<List<HorarioReserva>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public HorarioReserva Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/horarioReservas/{id}";
            var respuesta = _restClientHttp.Get<HorarioReserva>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(HorarioReserva horarioReserva)
        {
            string url = $"http://localhost/restaurant/api/horarioReservas/";
            var respuesta = _restClientHttp.Post<int>(url, horarioReserva);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(HorarioReserva horarioReserva, int idHorarioReserva)
        {
            string url = $"http://localhost/restaurant/api/horarioReservas/{idHorarioReserva}";
            var respuesta = _restClientHttp.Put<bool>(url, horarioReserva);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
