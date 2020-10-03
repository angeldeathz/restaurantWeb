using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class ArticuloPedidoService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/ArticuloPedidos/";

        public ArticuloPedidoService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<ArticuloPedido> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<ArticuloPedido>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public ArticuloPedido Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<ArticuloPedido>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(ArticuloPedido articuloPedido)
        {
            var respuesta = _restClientHttp.Post<int>(_url, articuloPedido);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(ArticuloPedido articuloPedido)
        {
            var respuesta = _restClientHttp.Put<bool>(_url, articuloPedido);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
