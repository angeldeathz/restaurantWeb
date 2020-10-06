using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class ProveedorService
    {
        private readonly RestClientHttp _restClientHttp;

        public ProveedorService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<Proveedor> Obtener()
        {
            string url = $"http://localhost/restaurant/api/proveedores/";
            var respuesta = _restClientHttp.Get<List<Proveedor>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Proveedor Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/proveedores/{id}";
            var respuesta = _restClientHttp.Get<Proveedor>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(Proveedor proveedor)
        {
            string url = $"http://localhost/restaurant/api/proveedores/";
            var respuesta = _restClientHttp.Post<int>(url, proveedor);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(Proveedor proveedor, int idProveedor)
        {
            string url = $"http://localhost/restaurant/api/proveedores/{idProveedor}";
            var respuesta = _restClientHttp.Put<bool>(url, proveedor);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
