using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class EstadoPedidoService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/estadoPedidos/";

        public EstadoPedidoService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<EstadoPedido> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<EstadoPedido>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public EstadoPedido Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<EstadoPedido>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(EstadoPedido estadoPedido)
        {
            var respuesta = _restClientHttp.Post<int>(_url, estadoPedido);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(EstadoPedido estadoPedido, int idEstadoPedido)
        {
            _url = $"{_url}{idEstadoPedido}";
            var respuesta = _restClientHttp.Put<bool>(_url, estadoPedido);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
