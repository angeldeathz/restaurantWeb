using System.Net;
using Restaurant.Model.Dto;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class UsuarioService
    {
        private readonly RestClientHttp _restClientHttp;

        public UsuarioService()
        {
            _restClientHttp = new RestClientHttp();
        }

        public Token Autenticar(string rut, string contrasena)
        {
            const string url = "http://localhost/Autenticacion/token";
            var respuesta = _restClientHttp.GetToken<Token>(url, rut, contrasena);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public object ObtenerPorRut(string rut)
        {
            string url = $"http://localhost/restaurant/api/usuarios?rut={rut}";
            var respuesta = _restClientHttp.Get<object>(url, rut);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }
    }
}
