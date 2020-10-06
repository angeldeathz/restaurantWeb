using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class DetalleOrdenProveedorService
    {
        private readonly RestClientHttp _restClientHttp;

        public DetalleOrdenProveedorService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<DetalleOrdenProveedor> Obtener()
        {
            string url = $"http://localhost/restaurant/api/DetalleOrdenProveedor/";
            var respuesta = _restClientHttp.Get<List<DetalleOrdenProveedor>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public DetalleOrdenProveedor Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/DetalleOrdenProveedor/{id}";
            var respuesta = _restClientHttp.Get<DetalleOrdenProveedor>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }
        public int Guardar(DetalleOrdenProveedor detalleOrdenProveedor)
        {
            string url = $"http://localhost/restaurant/api/DetalleOrdenProveedor/";
            var respuesta = _restClientHttp.Post<int>(url, detalleOrdenProveedor);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(DetalleOrdenProveedor detalleOrdenProveedor, int id)
        {
            string url = $"http://localhost/restaurant/api/DetalleOrdenProveedor/{id}";
            var respuesta = _restClientHttp.Put<bool>(url, detalleOrdenProveedor);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
