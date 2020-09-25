using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Restaurant.Services.Shared
{
    public class RestClientHttp
    {
        #region Get

        public T GetObject<T>(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(url).Result;
                return response.Content.ReadAsAsync<T>().Result;
            }
        }

        public T GetObject<T>(string url, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var response = httpClient.GetAsync(url).Result;
                return response.Content.ReadAsAsync<T>().Result;
            }
        }

        public string GetString(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(url).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public string GetString(string url, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var response = httpClient.GetAsync(url).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public RestClientResponse Get(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(url).Result;

                return new RestClientResponse
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.Content.ReadAsStringAsync().Result
                };
            }
        }

        public RestClientResponse Get(string url, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var response = httpClient.GetAsync(url).Result;

                return new RestClientResponse
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.Content.ReadAsStringAsync().Result
                };
            }
        }

        public RestClientResponse<T> Get<T>(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(url).Result;

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        response.Content.ReadAsAsync<T>().Result
                };

                return restClientResponse;
            }
        }

        public RestClientResponse<T> Get<T>(string url, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var response = httpClient.GetAsync(url).Result;

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        response.Content.ReadAsAsync<T>().Result
                };

                return restClientResponse;
            }
        }

        #endregion

        #region Post

        public RestClientResponse Post(string url, object objectToPost)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = httpClient.PostAsJsonAsync(url, objectToPost).Result;

                return new RestClientResponse
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.Content.ReadAsStringAsync().Result
                };
            }
        }

        public RestClientResponse Post(string url, object objectToPost, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = httpClient.PostAsJsonAsync(url, objectToPost).Result;

                return new RestClientResponse
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.Content.ReadAsStringAsync().Result
                };
            }
        }

        public RestClientResponse<T> Post<T>(string url, object objectToPost)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = httpClient.PostAsJsonAsync(url, objectToPost).Result;

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        response.Content.ReadAsAsync<T>().Result
                };

                return restClientResponse;
            }
        }

        public RestClientResponse<T> Post<T>(string url, object objectToPost, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = httpClient.PostAsJsonAsync(url, objectToPost).Result;

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        response.Content.ReadAsAsync<T>().Result
                };

                return restClientResponse;
            }
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
                        response.Content.ReadAsAsync<T>().Result
                };

                return restClientResponse;
            }
        }

        #endregion

        #region Delete

        public RestClientResponse Delete(string url)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = httpClient.DeleteAsync(url).Result;

                return new RestClientResponse
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.Content.ReadAsStringAsync().Result
                };
            }
        }

        public RestClientResponse Delete(string url, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = httpClient.DeleteAsync(url).Result;

                return new RestClientResponse
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.Content.ReadAsStringAsync().Result
                };
            }
        }

        public RestClientResponse<T> Delete<T>(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.DeleteAsync(url).Result;

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        response.Content.ReadAsAsync<T>().Result
                };

                return restClientResponse;
            }
        }

        public RestClientResponse<T> Delete<T>(string url, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var response = httpClient.DeleteAsync(url).Result;

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        response.Content.ReadAsAsync<T>().Result
                };

                return restClientResponse;
            }
        }

        #endregion

        #region Put

        public RestClientResponse Put(string url, object objectToPut)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = httpClient.PutAsJsonAsync(url, objectToPut).Result;

                return new RestClientResponse
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.Content.ReadAsStringAsync().Result
                };
            }
        }

        public RestClientResponse Put(string url, object objectToPut, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = httpClient.PutAsJsonAsync(url, objectToPut).Result;

                return new RestClientResponse
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.Content.ReadAsStringAsync().Result
                };
            }
        }

        public RestClientResponse<T> Put<T>(string url, object objectToPut)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = httpClient.PutAsJsonAsync(url, objectToPut).Result;

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        response.Content.ReadAsAsync<T>().Result
                };

                return restClientResponse;
            }
        }

        public RestClientResponse<T> Put<T>(string url, object objectToPut, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = httpClient.PutAsJsonAsync(url, objectToPut).Result;

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        response.Content.ReadAsAsync<T>().Result
                };

                return restClientResponse;
            }
        }

        #endregion
    }
}
