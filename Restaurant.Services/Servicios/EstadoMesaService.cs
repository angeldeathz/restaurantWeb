using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class EstadoMesaService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/estadoMesas/";

        public EstadoMesaService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<EstadoMesa> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<EstadoMesa>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public EstadoMesa Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<EstadoMesa>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(EstadoMesa estadoMesa)
        {
            var respuesta = _restClientHttp.Post<int>(_url, estadoMesa);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(EstadoMesa estadoMesa, int idEstadoMesa)
        {
            _url = $"{_url}{idEstadoMesa}";
            var respuesta = _restClientHttp.Put<bool>(_url, estadoMesa);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
