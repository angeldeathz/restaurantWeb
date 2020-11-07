using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class ClienteService
    {
        private readonly RestClientHttp _restClientHttp;

        public ClienteService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<Cliente> Obtener()
        {
            string url = $"http://localhost/restaurant/api/clientes/";
            var respuesta = _restClientHttp.Get<List<Cliente>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Cliente Obtener(int id)
        {
             string url = $"http://localhost/restaurant/api/clientes/{id}";
            var respuesta = _restClientHttp.Get<Cliente>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(Cliente cliente)
        {
            string url = $"http://localhost/restaurant/api/clientes/";
            var respuesta = _restClientHttp.Post<int>(url, cliente);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }
        public int GuardarBasico(Persona persona)
        {
            string url = $"http://localhost/restaurant/api/clientes/nuevos/";
            var respuesta = _restClientHttp.Post<int>(url, persona);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(Cliente cliente, int idCliente)
        {
            string url = $"http://localhost/restaurant/api/clientes/{idCliente}";
            var respuesta = _restClientHttp.Put<bool>(url, cliente);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
