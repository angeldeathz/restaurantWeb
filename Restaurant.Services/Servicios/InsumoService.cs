using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class InsumoService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/Insumos/";

        public InsumoService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<Insumo> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<Insumo>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public Insumo Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<Insumo>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(Insumo insumo)
        {
            var respuesta = _restClientHttp.Post<int>(_url, insumo);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(Insumo insumo, int idInsumo)
        {
            _url = $"{_url}{idInsumo}";
            var respuesta = _restClientHttp.Put<bool>(_url, insumo);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
