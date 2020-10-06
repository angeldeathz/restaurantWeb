using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class PedidoService
    {
        private readonly RestClientHttp _restClientHttp;

        public PedidoService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<Pedido> Obtener()
        {
            string url = $"http://localhost/restaurant/api/pedidos/";
            var respuesta = _restClientHttp.Get<List<Pedido>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Pedido Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/pedidos/{id}";
            var respuesta = _restClientHttp.Get<Pedido>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(Pedido pedido)
        {
            string url = $"http://localhost/restaurant/api/pedidos/";
            var respuesta = _restClientHttp.Post<int>(url, pedido);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(Pedido pedido, int idPedido)
        {
            string url = $"http://localhost/restaurant/api/pedidos/{idPedido}";
            var respuesta = _restClientHttp.Put<bool>(url, pedido);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
