using System.Net;
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

        public bool Autenticar(object autenticador)
        {
            var url = "http://localhost/VehiculosOnline/usuarios/api/usuarios/autenticar";
            var respuesta = _restClientHttp.Post<bool>(url, autenticador);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return true;
        }
    }
}
