using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class TipoConsumoService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/tipoConsumos/";

        public TipoConsumoService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<TipoConsumo> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<TipoConsumo>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public TipoConsumo Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<TipoConsumo>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }
        public int Guardar(TipoConsumo tipoConsumo)
        {
            var respuesta = _restClientHttp.Post<int>(_url, tipoConsumo);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(TipoConsumo tipoConsumo, int idTipoConsumo)
        {
            _url = $"{_url}{idTipoConsumo}";
            var respuesta = _restClientHttp.Put<bool>(_url, tipoConsumo);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
