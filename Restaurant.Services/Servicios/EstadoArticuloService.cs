﻿using System.Collections.Generic;
using System.Net;
using Restaurant.Model.Clases;
using Restaurant.Services.Shared;

namespace Restaurant.Services.Servicios
{
    public class EstadoArticuloService
    {
        private readonly RestClientHttp _restClientHttp;
        private string _url = $"http://localhost/restaurant/api/estadoArticulos/";

        public EstadoArticuloService()
        {
            _restClientHttp = new RestClientHttp();
        }

        public List<EstadoArticulo> Obtener()
        {
            var respuesta = _restClientHttp.Get<List<EstadoArticulo>>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }

        public EstadoArticulo Obtener(int id)
        {
            _url = $"{_url}{id}";
            var respuesta = _restClientHttp.Get<EstadoArticulo>(_url);
            if (respuesta.StatusName != HttpStatusCode.OK) return null;
            return respuesta.Response;
        }
    }
}