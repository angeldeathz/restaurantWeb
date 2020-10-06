using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Model.Dto;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class UsuarioService
    {
        private readonly RestClientHttp _restClientHttp;

        public UsuarioService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public Token Autenticar(string rut, string contrasena)
        {
            var url = "http://localhost/Autenticacion/token";
            var respuesta = _restClientHttp.GetToken<Token>(url, rut, contrasena);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Usuario ObtenerPorRut(string rut)
        {
            string url = $"http://localhost/restaurant/api/usuarios?rut={rut}";
            var respuesta = _restClientHttp.Get<Usuario>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public List<Usuario> Obtener()
        {
            string url = $"http://localhost/restaurant/api/usuarios";
            var respuesta = _restClientHttp.Get<List<Usuario>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Usuario Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/usuarios/{id}";
            var respuesta = _restClientHttp.Get<Usuario>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(Usuario usuario)
        {
            string url = $"http://localhost/restaurant/api/usuarios";
            var respuesta = _restClientHttp.Post<int>(url, usuario);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(Usuario usuario, int idUsuario)
        {
            string url = $"http://localhost/restaurant/api/usuarios/{idUsuario}";
            var respuesta = _restClientHttp.Put<bool>(url, usuario);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
