using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class UnidadDeMedidaService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/UnidadesDeMedida/";

        public UnidadDeMedidaService()
        {
            _restClientHttp = new RestClientHttp();
        }

        public List<UnidadMedida> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<UnidadMedida>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public UnidadMedida Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<UnidadMedida>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }
    }
}
