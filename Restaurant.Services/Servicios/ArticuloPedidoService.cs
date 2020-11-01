using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class ArticuloPedidoService
    {
        private readonly RestClientHttp _restClientHttp;

        public ArticuloPedidoService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<ArticuloPedido> Obtener()
        {
            string url = $"http://localhost/restaurant/api/articuloPedidos/";
            var respuesta = _restClientHttp.Get<List<ArticuloPedido>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public ArticuloPedido Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/articuloPedidos/{id}";
            var respuesta = _restClientHttp.Get<ArticuloPedido>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(ArticuloPedido articuloPedido)
        {
            string url = "http://localhost/restaurant/api/articuloPedidos";
            var respuesta = _restClientHttp.Post<int>(url, articuloPedido);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(ArticuloPedido articuloPedido, int id)
        {
            string url = $"http://localhost/restaurant/api/articuloPedidos/{id}";
            var respuesta = _restClientHttp.Put<bool>(url, articuloPedido);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
