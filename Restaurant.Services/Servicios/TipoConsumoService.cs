using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class TipoConsumoService
    {
        private readonly RestClientHttp _restClientHttp;

        public TipoConsumoService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<TipoConsumo> Obtener()
        {
            string url = $"http://localhost/restaurant/api/tipoConsumos/";
            var respuesta = _restClientHttp.Get<List<TipoConsumo>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public TipoConsumo Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/tipoConsumos/{id}";
            var respuesta = _restClientHttp.Get<TipoConsumo>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }
        public int Guardar(TipoConsumo tipoConsumo)
        {
            string url = $"http://localhost/restaurant/api/tipoConsumos/";
            var respuesta = _restClientHttp.Post<int>(url, tipoConsumo);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(TipoConsumo tipoConsumo, int idTipoConsumo)
        {
            string url = $"http://localhost/restaurant/api/tipoConsumos/{idTipoConsumo}";
            var respuesta = _restClientHttp.Put<bool>(url, tipoConsumo);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
