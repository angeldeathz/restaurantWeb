using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class OrdenProveedorService
    {
        private readonly RestClientHttp _restClientHttp;

        public OrdenProveedorService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<OrdenProveedor> Obtener()
        {
            string url = $"http://localhost/restaurant/api/ordenesProveedor/";
            var respuesta = _restClientHttp.Get<List<OrdenProveedor>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public OrdenProveedor Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/ordenesProveedor/{id}";
            var respuesta = _restClientHttp.Get<OrdenProveedor>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(OrdenProveedor pedido)
        {
            string url = $"http://localhost/restaurant/api/ordenesProveedor/";
            var respuesta = _restClientHttp.Post<int>(url, pedido);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(OrdenProveedor pedido, int idOrdenProveedor)
        {
            string url = $"http://localhost/restaurant/api/ordenesProveedor/{idOrdenProveedor}";
            var respuesta = _restClientHttp.Put<bool>(url, pedido);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
