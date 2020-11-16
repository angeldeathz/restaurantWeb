using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class TipoDocumentoPagoService
    {
        private readonly RestClientHttp _restClientHttp;

        public TipoDocumentoPagoService(string token)
        {
            _restClientHttp = new RestClientHttp(token);
        }

        public List<TipoDocumentoPago> Obtener()
        {
            string url = $"http://localhost/restaurant/api/tipoDocumentosPago/";
            var respuesta = _restClientHttp.Get<List<TipoDocumentoPago>>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public TipoDocumentoPago Obtener(int id)
        {
            string url = $"http://localhost/restaurant/api/tipoDocumentosPago/{id}";
            var respuesta = _restClientHttp.Get<TipoDocumentoPago>(url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public int Guardar(TipoDocumentoPago tipoDocumentoPago)
        {
            string url = $"http://localhost/restaurant/api/tipoDocumentosPago/";
            var respuesta = _restClientHttp.Post<int>(url, tipoDocumentoPago);
            if (respuesta.StatusName != HttpStatusCode.OK) return 0;
            return respuesta.Response;
        }

        public bool Modificar(TipoDocumentoPago tipoDocumentoPago, int idTipoDocumentoPago)
        {
            string url = $"http://localhost/restaurant/api/tipoDocumentosPago/{idTipoDocumentoPago}";
            var respuesta = _restClientHttp.Put<bool>(url, tipoDocumentoPago);
            if (respuesta.StatusName != HttpStatusCode.OK) return false;
            return respuesta.Response;
        }
    }
}
