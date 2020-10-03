using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class PedidoService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/pedidos/";

        public PedidoService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<Pedido> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<Pedido>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Pedido Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<Pedido>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(Pedido pedido)
        {
            var respuesta = _restClientHttp.Post<int>(_url, pedido);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(Pedido pedido, int idPedido)
        {
            _url = $"{_url}{idPedido}";
            var respuesta = _restClientHttp.Put<bool>(_url, pedido);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
