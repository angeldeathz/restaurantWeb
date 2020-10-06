using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class InsumoService
    {
        private readonly RestClientHttp _restClientHttp;

        public InsumoService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<Insumo> Obtener()
        {
            var url = $"http://localhost/restaurant/api/Insumos/";
            var respuesta = _restClientHttp.Get<List<Insumo>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Insumo Obtener(int id)
        {
            var url = $"http://localhost/restaurant/api/Insumos/{id}";
            var respuesta = _restClientHttp.Get<Insumo>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(Insumo insumo)
        {
            var url = $"http://localhost/restaurant/api/Insumos/";
            var respuesta = _restClientHttp.Post<int>(url, insumo);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(Insumo insumo, int idInsumo)
        {
            var url = $"http://localhost/restaurant/api/Insumos/{idInsumo}";
            var respuesta = _restClientHttp.Put<bool>(url, insumo);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
