using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class ArticuloConsumoDirectoService
    {
        private readonly RestClientHttp _restClientHttp;

        public ArticuloConsumoDirectoService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<ArticuloConsumoDirecto> Obtener()
        {
            string url = $"http://localhost/restaurant/api/ArticulosConsumoDirecto/";
            var respuesta = _restClientHttp.Get<List<ArticuloConsumoDirecto>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public ArticuloConsumoDirecto Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/ArticulosConsumoDirecto/{id}";
            var respuesta = _restClientHttp.Get<ArticuloConsumoDirecto>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(ArticuloConsumoDirecto ArticuloConsumoDirecto)
        {
            string url = $"http://localhost/restaurant/api/ArticulosConsumoDirecto/";
            var respuesta = _restClientHttp.Post<int>(url, ArticuloConsumoDirecto);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(ArticuloConsumoDirecto ArticuloConsumoDirecto, int idArticuloConsumoDirecto)
        {
            string url = $"http://localhost/restaurant/api/ArticulosConsumoDirecto/{idArticuloConsumoDirecto}";
            var respuesta = _restClientHttp.Put<bool>(url, ArticuloConsumoDirecto);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
