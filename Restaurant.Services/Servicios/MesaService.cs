using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class MesaService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/mesas/";

        public MesaService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<Mesa> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<Mesa>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Mesa Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<Mesa>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(Mesa mesa)
        {
            var respuesta = _restClientHttp.Post<int>(_url, mesa);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(Mesa mesa, int idMesa)
        {
            _url = $"{_url}{idMesa}";
            var respuesta = _restClientHttp.Put<bool>(_url, mesa);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
