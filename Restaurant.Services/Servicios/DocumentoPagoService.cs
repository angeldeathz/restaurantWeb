using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class DocumentoPagoService
    {
        private readonly RestClientHttp _restClientHttp;

        public DocumentoPagoService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<DocumentoPago> Obtener()
        {
            string url = $"http://localhost/restaurant/api/documentoPagos/";
            var respuesta = _restClientHttp.Get<List<DocumentoPago>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public DocumentoPago Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/documentoPagos/{id}";
            var respuesta = _restClientHttp.Get<DocumentoPago>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(DocumentoPago documentoPago)
        {
            string url = $"http://localhost/restaurant/api/documentoPagos/";
            var respuesta = _restClientHttp.Post<int>(url, documentoPago);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(DocumentoPago documentoPago, int idDocumentoPago)
        {
            string url = $"http://localhost/restaurant/api/documentoPagos/{idDocumentoPago}";
            var respuesta = _restClientHttp.Put<bool>(url, documentoPago);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
