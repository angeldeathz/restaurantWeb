using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class TipoUsuarioService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/tipoUsuarios/";

        public TipoUsuarioService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<TipoUsuario> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<TipoUsuario>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public TipoUsuario Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<TipoUsuario>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }
    }
}
