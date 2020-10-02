using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class ClienteService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/clientes/";

        public ClienteService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<Cliente> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<Cliente>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Cliente Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<Cliente>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(Cliente cliente)
        {
            var respuesta = _restClientHttp.Post<int>(_url, cliente);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(Cliente cliente)
        {
            var respuesta = _restClientHttp.Put<bool>(_url, cliente);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
