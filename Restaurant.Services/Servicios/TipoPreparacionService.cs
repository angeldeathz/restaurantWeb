using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class TipoPreparacionService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/tipopreparaciones/";

        public TipoPreparacionService()
        {
            _restClientHttp = new RestClientHttp();
        }

        public List<TipoPreparacion> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<TipoPreparacion>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public TipoPreparacion Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<TipoPreparacion>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }
    }
}
