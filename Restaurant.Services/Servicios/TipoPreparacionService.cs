using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class TipoPreparacionService
    {
        private readonly RestClientHttp _restClientHttp;

        public TipoPreparacionService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<TipoPreparacion> Obtener()
        {
            string url = $"http://localhost/restaurant/api/tipopreparaciones/";
            var respuesta = _restClientHttp.Get<List<TipoPreparacion>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public TipoPreparacion Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/tipopreparaciones/{id}";
            var respuesta = _restClientHttp.Get<TipoPreparacion>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }
    }
}
