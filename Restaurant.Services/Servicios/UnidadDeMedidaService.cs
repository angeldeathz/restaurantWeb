using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class UnidadDeMedidaService
    {
        private readonly RestClientHttp _restClientHttp;

        public UnidadDeMedidaService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<UnidadMedida> Obtener()
        {
            string url = $"http://localhost/restaurant/api/UnidadesDeMedida/";
            var respuesta = _restClientHttp.Get<List<UnidadMedida>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public UnidadMedida Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/UnidadesDeMedida/{id}";
            var respuesta = _restClientHttp.Get<UnidadMedida>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }
    }
}
