using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class TipoUsuarioService
    {
        private readonly RestClientHttp _restClientHttp;

        public TipoUsuarioService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<TipoUsuario> Obtener()
        {
            string url = $"http://localhost/restaurant/api/tipoUsuarios/";
            var respuesta = _restClientHttp.Get<List<TipoUsuario>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public TipoUsuario Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/tipoUsuarios/{id}";
            var respuesta = _restClientHttp.Get<TipoUsuario>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }
    }
}
