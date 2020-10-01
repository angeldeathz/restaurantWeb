using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Model.Dto;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class UsuarioService
    {
        private RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/usuarios";
        public string Token { get; set; }

        public UsuarioService()
        {
            _restClientHttp = new RestClientHttp(Token);
        }

        public Token Autenticar(string rut, string contrasena)
        {
            const string url = "http://localhost/Autenticacion/token";
            var respuesta = _restClientHttp.GetToken<Token>(url, rut, contrasena);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Usuario ObtenerPorRut(string rut)
        {
            _url = $"{_url}?rut={rut}";
            _restClientHttp = new RestClientHttp(Token);
            var respuesta = _restClientHttp.Get<Usuario>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public List<Usuario> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<Usuario>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Usuario Obtener(int id)
        {
            _url = $"{_url}/{id}";
            var respuesta = _restClientHttp.Get<Usuario>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(Usuario usuario)
        {
            var respuesta = _restClientHttp.Post<int>(_url, usuario);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(Usuario usuario)
        {
            var respuesta = _restClientHttp.Put<bool>(_url, usuario);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
