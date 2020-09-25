﻿using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Restaurant.Services.Shared
{
    public class RestClientHttp
    {
        #region Get

        #region Async

        public async Task<T> GetObjectAsync<T>(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                return await response.Content.ReadAsAsync<T>();
            }
        }

        public async Task<T> GetObjectAsync<T>(string url, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var response = await httpClient.GetAsync(url);
                return await response.Content.ReadAsAsync<T>();
            }
        }

        public async Task<string> GetStringAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> GetStringAsync(string url, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var response = await httpClient.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<RestClientResponse> GetAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);

                return new RestClientResponse
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = await response.Content.ReadAsStringAsync()
                };
            }
        }

        public async Task<RestClientResponse> GetAsync(string url, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var response = await httpClient.GetAsync(url);

                return new RestClientResponse
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = await response.Content.ReadAsStringAsync()
                };
            }
        }

        public async Task<RestClientResponse<T>> GetAsync<T>(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        await response.Content.ReadAsAsync<T>()
                };

                return restClientResponse;
            }
        }

        public async Task<RestClientResponse<T>> GetAsync<T>(string url, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var response = await httpClient.GetAsync(url);

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        await response.Content.ReadAsAsync<T>()
                };

                return restClientResponse;
            }
        }

        #endregion

        #region Sync

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

        #endregion

        #region Post

        #region Async

        public async Task<RestClientResponse> PostAsync(string url, object objectToPost)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = await httpClient.PostAsJsonAsync(url, objectToPost);

                return new RestClientResponse
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = await response.Content.ReadAsStringAsync()
                };
            }
        }

        public async Task<RestClientResponse> PostAsync(string url, object objectToPost, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = await httpClient.PostAsJsonAsync(url, objectToPost);

                return new RestClientResponse
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = await response.Content.ReadAsStringAsync()
                };
            }
        }

        public async Task<RestClientResponse<T>> PostAsync<T>(string url, object objectToPost)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = await httpClient.PostAsJsonAsync(url, objectToPost);

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        await response.Content.ReadAsAsync<T>()
                };

                return restClientResponse;
            }
        }

        public async Task<RestClientResponse<T>> PostAsync<T>(string url, object objectToPost, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = await httpClient.PostAsJsonAsync(url, objectToPost);

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        await response.Content.ReadAsAsync<T>()
                };

                return restClientResponse;
            }
        }

        #endregion

        #region Sync

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

        #endregion

        #endregion

        #region Delete

        #region Async

        public async Task<RestClientResponse> DeleteAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = await httpClient.DeleteAsync(url);

                return new RestClientResponse
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = await response.Content.ReadAsStringAsync()
                };
            }
        }

        public async Task<RestClientResponse> DeleteAsync(string url, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = await httpClient.DeleteAsync(url);

                return new RestClientResponse
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = await response.Content.ReadAsStringAsync()
                };
            }
        }

        public async Task<RestClientResponse<T>> DeleteAsync<T>(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(url);

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        await response.Content.ReadAsAsync<T>()
                };

                return restClientResponse;
            }
        }

        public async Task<RestClientResponse<T>> DeleteAsync<T>(string url, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var response = await httpClient.DeleteAsync(url);

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        await response.Content.ReadAsAsync<T>()
                };

                return restClientResponse;
            }
        }

        #endregion

        #region Sync

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

        #endregion

        #region Put

        #region Async

        public async Task<RestClientResponse> PutAsync(string url, object objectToPut)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = await httpClient.PutAsJsonAsync(url, objectToPut);

                return new RestClientResponse
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = await response.Content.ReadAsStringAsync()
                };
            }
        }

        public async Task<RestClientResponse> PutAsync(string url, object objectToPut, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = await httpClient.PutAsJsonAsync(url, objectToPut);

                return new RestClientResponse
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = await response.Content.ReadAsStringAsync()
                };
            }
        }

        public async Task<RestClientResponse<T>> PutAsync<T>(string url, object objectToPut)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = await httpClient.PutAsJsonAsync(url, objectToPut);

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        await response.Content.ReadAsAsync<T>()
                };

                return restClientResponse;
            }
        }

        public async Task<RestClientResponse<T>> PutAsync<T>(string url, object objectToPut, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
                var response = await httpClient.PutAsJsonAsync(url, objectToPut);

                var restClientResponse = new RestClientResponse<T>
                {
                    StatusCode = (int)response.StatusCode,
                    StatusName = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Response = response.StatusCode != HttpStatusCode.OK ?
                        default(T) :
                        await response.Content.ReadAsAsync<T>()
                };

                return restClientResponse;
            }
        }

        #endregion

        #region Sync

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

        #endregion
    }
}
