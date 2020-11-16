using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class MedioPagoDocumentoService
    {
        private readonly RestClientHttp _restClientHttp;

        public MedioPagoDocumentoService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<MedioPagoDocumento> Obtener()
        {
            string url = $"http://localhost/restaurant/api/medioPagoDocumentos/";
            var respuesta = _restClientHttp.Get<List<MedioPagoDocumento>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public MedioPagoDocumento Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/medioPagoDocumentos/{id}";
            var respuesta = _restClientHttp.Get<MedioPagoDocumento>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(MedioPagoDocumento medioPagoDocumento)
        {
            string url = $"http://localhost/restaurant/api/medioPagoDocumentos/";
            var respuesta = _restClientHttp.Post<int>(url, medioPagoDocumento);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(MedioPagoDocumento medioPagoDocumento, int idMedioPagoDocumento)
        {
            string url = $"http://localhost/restaurant/api/medioPagoDocumentos/{idMedioPagoDocumento}";
            var respuesta = _restClientHttp.Put<bool>(url, medioPagoDocumento);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
