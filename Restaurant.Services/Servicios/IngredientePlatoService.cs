using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class IngredientePlatoService
    {
        private readonly RestClientHttp _restClientHttp;

        public IngredientePlatoService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<IngredientePlato> Obtener()
        {
            string url = $"http://localhost/restaurant/api/IngredientesPlato/";
            var respuesta = _restClientHttp.Get<List<IngredientePlato>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public IngredientePlato Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/IngredientePlatos/{id}";
            var respuesta = _restClientHttp.Get<IngredientePlato>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(IngredientePlato IngredientePlato)
        {
            string url = $"http://localhost/restaurant/api/IngredientePlatos/";
            var respuesta = _restClientHttp.Post<int>(url, IngredientePlato);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(IngredientePlato IngredientePlato, int id)
        {
            string url = $"http://localhost/restaurant/api/IngredientePlatos/{id}";
            var respuesta = _restClientHttp.Put<bool>(url, IngredientePlato);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
