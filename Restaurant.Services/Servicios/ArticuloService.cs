using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class ArticuloService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/articulos/";

        public ArticuloService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<Articulo> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<Articulo>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Articulo Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<Articulo>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(Articulo articulo)
        {
            var respuesta = _restClientHttp.Post<int>(_url, articulo);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(Articulo articulo, int idArticulo)
        {
            _url = $"{_url}{idArticulo}";
            var respuesta = _restClientHttp.Put<bool>(_url, articulo);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
