using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class MesaService
    {
        private readonly RestClientHttp _restClientHttp;

        public MesaService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<Mesa> Obtener()
        {
            string url = $"http://localhost/restaurant/api/mesas/";
            var respuesta = _restClientHttp.Get<List<Mesa>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Mesa Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/mesas/{id}";
            var respuesta = _restClientHttp.Get<Mesa>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(Mesa mesa)
        {
            string url = $"http://localhost/restaurant/api/mesas/";
            var respuesta = _restClientHttp.Post<int>(url, mesa);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(Mesa mesa, int idMesa)
        {
            string url = $"http://localhost/restaurant/api/mesas/{idMesa}";
            var respuesta = _restClientHttp.Put<bool>(url, mesa);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
