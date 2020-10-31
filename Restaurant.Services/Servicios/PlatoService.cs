using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class PlatoService
    {
        private readonly RestClientHttp _restClientHttp;

        public PlatoService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<Plato> Obtener()
        {
            string url = $"http://localhost/restaurant/api/platos/";
            var respuesta = _restClientHttp.Get<List<Plato>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Plato Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/platos/{id}";
            var respuesta = _restClientHttp.Get<Plato>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(Plato Plato)
        {
            string url = $"http://localhost/restaurant/api/platos/";
            var respuesta = _restClientHttp.Post<int>(url, Plato);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(Plato Plato, int idPlato)
        {
            string url = $"http://localhost/restaurant/api/platos/{idPlato}";
            var respuesta = _restClientHttp.Put<bool>(url, Plato);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
