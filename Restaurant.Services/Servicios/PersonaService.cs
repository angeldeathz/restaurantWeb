using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class PersonaService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/personas";

        public PersonaService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<Persona> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<Persona>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Persona Obtener(int id)
        {
            _url = $"{_url}/{id}";
            var respuesta = _restClientHttp.Get<Persona>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Persona Obtener(string rut)
        {
            _url = $"{_url}?rut={rut}";
            var respuesta = _restClientHttp.Get<Persona>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }
    }
}
