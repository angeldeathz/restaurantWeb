using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class ArticuloService
    {
        private readonly RestClientHttp _restClientHttp;

        public ArticuloService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<Articulo> Obtener()
        {
            string url = $"http://localhost/restaurant/api/articulos/";
            var respuesta = _restClientHttp.Get<List<Articulo>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Articulo Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/articulos/{id}";
            var respuesta = _restClientHttp.Get<Articulo>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(Articulo articulo)
        {
            string url = $"http://localhost/restaurant/api/articulos/";
            var respuesta = _restClientHttp.Post<int>(url, articulo);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(Articulo articulo, int idArticulo)
        {
            string url = $"http://localhost/restaurant/api/articulos/{idArticulo}";
            var respuesta = _restClientHttp.Put<bool>(url, articulo);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
