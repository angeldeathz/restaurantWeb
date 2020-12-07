using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Restaurant.Services.Shared
{
    public class RestClientHttp
    {
        private readonly string _token;

        public RestClientHttp(string token)
        {
            _token = token;
        }

        public RestClientResponse<T> GetToken<T>(string url, string rut, string contrasena)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var parameters = new Dictionary<string, string>
                {
                    {"grant_type", "password"},
                    {"username", rut},
                    {"password", contrasena}
                };
                var response = httpClient.PostAsync(url, new FormUrlEncodedContent(parameters)).Result;

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result)
                };

                switch (response.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        restClientResponse.Error =
                            JsonConvert.DeserializeObject<ErrorResponse>(response.Content.ReadAsStringAsync().Result);
                        break;
                }

                return restClientResponse;
            }
        }

        public RestClientResponse<T> GetToken<T>(string url)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var parameters = new Dictionary<string, string>
                {
                    {"grant_type", "password"},
                    {"user_type", "cliente"}
                };
                var response = httpClient.PostAsync(url, new FormUrlEncodedContent(parameters)).Result;

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result)
                };

                switch (response.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        restClientResponse.Error =
                            JsonConvert.DeserializeObject<ErrorResponse>(response.Content.ReadAsStringAsync().Result);
                        break;
                }

                return restClientResponse;
            }
        }

        public RestClientResponse<T> Get<T>(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var message = new HttpRequestMessage(HttpMethod.Get, url);
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var response = httpClient.SendAsync(message).Result;
                var e = response.Content.ReadAsStringAsync().Result;
                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result)
                };

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("El token está vacio o expirado");
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        restClientResponse.Error =
                            JsonConvert.DeserializeObject<ErrorResponse>(response.Content.ReadAsStringAsync().Result);
                        break;
                }

                if(restClientResponse.Error != null && restClientResponse.Error.Errores != null && restClientResponse.Error.Errores.Count > 0)
                {
                    string errores = String.Join(", ", restClientResponse.Error.Errores.ToArray());
                    throw new Exception(errores);
                }

                return restClientResponse;
            }
        }

        public RestClientResponse<T> Post<T>(string url, object objectToPost)
        {
            using (var httpClient = new HttpClient())
            {
                var message = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(objectToPost), Encoding.UTF8, "application/json")
                };
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var response = httpClient.SendAsync(message).Result;

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        response.Content.ReadAsAsync<T>().Result
                };

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("El token está vacio o expirado");
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        restClientResponse.Error =
                            JsonConvert.DeserializeObject<ErrorResponse>(response.Content.ReadAsStringAsync().Result);
                        break;
                }

                if (restClientResponse.Error != null && restClientResponse.Error.Errores != null && restClientResponse.Error.Errores.Count > 0)
                {
                    string errores = String.Join(", ", restClientResponse.Error.Errores.ToArray());
                    throw new Exception(errores);
                }

                return restClientResponse;
            }
        }

        public RestClientResponse<T> Delete<T>(string url)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _token);
                var response = httpClient.DeleteAsync(url).Result;

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result)
                };

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("El token está vacio o expirado");
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        restClientResponse.Error =
                            JsonConvert.DeserializeObject<ErrorResponse>(response.Content.ReadAsStringAsync().Result);
                        break;
                }

                if (restClientResponse.Error != null && restClientResponse.Error.Errores != null && restClientResponse.Error.Errores.Count > 0)
                {
                    string errores = String.Join(", ", restClientResponse.Error.Errores.ToArray());
                    throw new Exception(errores);
                }

                return restClientResponse;
            }
        }

        public RestClientResponse<T> Put<T>(string url, object objectToPut)
        {
            using (var httpClient = new HttpClient())
            {
                var message = new HttpRequestMessage(HttpMethod.Put, url)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(objectToPut), Encoding.UTF8, "application/json")
                };
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var response = httpClient.SendAsync(message).Result;

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result)
                };

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new Exception("El token está vacio o expirado");
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        restClientResponse.Error = JsonConvert.DeserializeObject<ErrorResponse>(response.Content.ReadAsStringAsync().Result);
                        break;
                }

                if (restClientResponse.Error != null && restClientResponse.Error.Errores != null && restClientResponse.Error.Errores.Count > 0)
                {
                    string errores = String.Join(", ", restClientResponse.Error.Errores.ToArray());
                    throw new Exception(errores);
                }

                return restClientResponse;
            }
        }
    }
}
