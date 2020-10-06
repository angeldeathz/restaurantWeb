using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class EstadoPedidoService
    {
        private readonly RestClientHttp _restClientHttp;

        public EstadoPedidoService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<EstadoPedido> Obtener()
        {
            string url = $"http://localhost/restaurant/api/estadoPedidos/";
            var respuesta = _restClientHttp.Get<List<EstadoPedido>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public EstadoPedido Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/estadoPedidos/{id}";
            var respuesta = _restClientHttp.Get<EstadoPedido>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(EstadoPedido estadoPedido)
        {
            string url = $"http://localhost/restaurant/api/estadoPedidos/";
            var respuesta = _restClientHttp.Post<int>(url, estadoPedido);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(EstadoPedido estadoPedido, int idEstadoPedido)
        {
            string url = $"http://localhost/restaurant/api/estadoPedidos/{idEstadoPedido}";
            var respuesta = _restClientHttp.Put<bool>(url, estadoPedido);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
