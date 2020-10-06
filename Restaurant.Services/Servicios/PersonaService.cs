using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class PersonaService
    {
        private readonly RestClientHttp _restClientHttp;

        public PersonaService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<Persona> Obtener()
        {
            string url = $"http://localhost/restaurant/api/personas";
            var respuesta = _restClientHttp.Get<List<Persona>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Persona Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/personas/{id}";
            var respuesta = _restClientHttp.Get<Persona>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Persona Obtener(string rut)
        {
            string url = $"http://localhost/restaurant/api/personas?rut={rut}";
            var respuesta = _restClientHttp.Get<Persona>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }
    }
}
