using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class OrdenProveedorService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/ordenesProveedor/";

        public OrdenProveedorService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<OrdenProveedor> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<OrdenProveedor>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public OrdenProveedor Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<OrdenProveedor>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(OrdenProveedor pedido)
        {
            var respuesta = _restClientHttp.Post<int>(_url, pedido);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(OrdenProveedor pedido, int idOrdenProveedor)
        {
            _url = $"{_url}{idOrdenProveedor}";
            var respuesta = _restClientHttp.Put<bool>(_url, pedido);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
