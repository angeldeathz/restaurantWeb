using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class DetalleOrdenProveedorService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/DetalleOrdenProveedor/";

        public DetalleOrdenProveedorService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<DetalleOrdenProveedor> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<DetalleOrdenProveedor>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public DetalleOrdenProveedor Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<DetalleOrdenProveedor>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }
        public int Guardar(DetalleOrdenProveedor DetalleOrdenProveedor)
        {
            var respuesta = _restClientHttp.Post<int>(_url, DetalleOrdenProveedor);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(DetalleOrdenProveedor DetalleOrdenProveedor)
        {
            var respuesta = _restClientHttp.Put<bool>(_url, DetalleOrdenProveedor);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
