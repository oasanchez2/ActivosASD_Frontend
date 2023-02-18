using GrupoASD.GestionActivos.App.Servicios.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GrupoASD.GestionActivos.App.Servicios.Facade
{
    public interface IAsdGestionActivosApi
    {
        /// <summary>
        ///  Realiza la conexión al api, metodo obtener todos los activos.
        /// </summary>
        /// <returns></returns>
        Task<RespuestaApi> ActivosObtenerAsync();

        /// <summary>
        /// Obtiene un activo por us id
        /// </summary>
        /// <returns></returns>
        Task<RespuestaApi> ActivosObtenerPorIdAsync(int id);
        /// <summary>
        /// Realiza la conexión al api y crea un activo nuevo
        /// </summary>
        /// <param name="activo"></param>
        /// <returns></returns>
        Task<RespuestaApi> ActivosInsertar(ActivosModel activo);
        /// <summary>
        /// Realiza la conexión al api, metodo actualizar un activo.
        /// </summary>
        /// <param name="activosUpdate"></param>
        /// <returns></returns>
        Task<RespuestaApi> ActivosUpdate(ActivosUpdate activosUpdate);
        /// <summary>
        /// Realiza la conexión al api, realiza busqueda personalizada segun parametros enviados.
        /// </summary>
        /// <param name="activosBusquedaModel"></param>
        /// <returns></returns>
        Task<RespuestaApi> ActivosBusqueda(ActivosBusquedaModel activosBusquedaModel);
    }
    public class AsdGestionActivosApi : IAsdGestionActivosApi
    {
        private readonly HttpClient _httpclient;

        public AsdGestionActivosApi(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        
        /// <summary>
        ///   Realiza la conexión al api, metodo obtener todos los activos.
        /// </summary>
        /// <returns></returns>
        public async Task<RespuestaApi> ActivosObtenerAsync()
        {
            // CONSTRUIMOS LA URL DE LA ACCIÓN
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append(_httpclient.BaseAddress != null ? _httpclient.BaseAddress.AbsoluteUri.TrimEnd('/') : "")
                       .Append("/api/activos");
            var url = urlBuilder_.ToString();
            try
            {
                using (var request = new HttpRequestMessage())
                {
                    ///////////////////////////////////////
                    // CONSTRUIMOS LA PETICIÓN (REQUEST) //
                    ///////////////////////////////////////

                    // DEFINIMOS EL MÉTODO HTTP
                    request.Method = new HttpMethod("GET");

                    // DEFINIMOS LA URI
                    request.RequestUri = new Uri(url, System.UriKind.RelativeOrAbsolute);

                    /////////////////////////////////////////
                    // CONSTRUIMOS LA RESPUESTA (RESPONSE) //
                    /////////////////////////////////////////

                    // Utilizamos ConfigureAwait(false) para evitar el DeadLock.
                    var respuesta = await _httpclient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                    // OBTENEMOS EL Content DEL RESPONSE como un String
                    // Utilizamos ConfigureAwait(false) para evitar el DeadLock.
                    var responseText_ = await respuesta.Content.ReadAsStringAsync().ConfigureAwait(false);

                    // DESERIALIZAMOS Content DEL RESPONSE                       
                    var responsejson = JsonConvert.DeserializeObject(responseText_, new JsonSerializerSettings { Formatting = Formatting.None });

                    RespuestaApi responseBody_ = new RespuestaApi
                    {
                        HttpStatus = respuesta.StatusCode,
                        JsonResultado = (responsejson != null) ? responsejson.ToString() : string.Empty
                    };
                    return responseBody_;
                }
            }
            finally
            {

            }
        }

        /// <summary>
        /// Obtiene un activo por us id
        /// </summary>
        /// <returns></returns>
        public async Task<RespuestaApi> ActivosObtenerPorIdAsync(int id)
        {
            // CONSTRUIMOS LA URL DE LA ACCIÓN
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append(_httpclient.BaseAddress != null ? _httpclient.BaseAddress.AbsoluteUri.TrimEnd('/') : "")
                       .Append("/api/activos/")
                       .Append(id);
            var url = urlBuilder_.ToString();
            try
            {
                using (var request = new HttpRequestMessage())
                {
                    ///////////////////////////////////////
                    // CONSTRUIMOS LA PETICIÓN (REQUEST) //
                    ///////////////////////////////////////

                    // DEFINIMOS EL MÉTODO HTTP
                    request.Method = new HttpMethod("GET");

                    // DEFINIMOS LA URI
                    request.RequestUri = new Uri(url, System.UriKind.RelativeOrAbsolute);

                    /////////////////////////////////////////
                    // CONSTRUIMOS LA RESPUESTA (RESPONSE) //
                    /////////////////////////////////////////

                    // Utilizamos ConfigureAwait(false) para evitar el DeadLock.
                    var respuesta = await _httpclient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                    // OBTENEMOS EL Content DEL RESPONSE como un String
                    // Utilizamos ConfigureAwait(false) para evitar el DeadLock.
                    var responseText_ = await respuesta.Content.ReadAsStringAsync().ConfigureAwait(false);

                    // DESERIALIZAMOS Content DEL RESPONSE                       
                    var responsejson = JsonConvert.DeserializeObject(responseText_, new JsonSerializerSettings { Formatting = Formatting.None });

                    RespuestaApi responseBody_ = new RespuestaApi
                    {
                        HttpStatus = respuesta.StatusCode,
                        JsonResultado = (responsejson != null) ? responsejson.ToString() : string.Empty
                    };
                    return responseBody_;
                }
            }
            finally
            {

            }
        }
        /// <summary>
        /// Realiza la conexión al api y crea un activo nuevo
        /// </summary>
        /// <param name="activo"></param>
        /// <returns></returns>
        public async Task<RespuestaApi> ActivosInsertar(ActivosModel activo)
        {
            // CONSTRUIMOS LA URL DE LA ACCIÓN
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append(_httpclient.BaseAddress != null ? _httpclient.BaseAddress.AbsoluteUri.TrimEnd('/') : "")
                       .Append("/api/activos");
            var url = urlBuilder_.ToString();
            try
            {
                using (var request = new HttpRequestMessage())
                {
                    ///////////////////////////////////////
                    // CONSTRUIMOS LA PETICIÓN (REQUEST) //
                    ///////////////////////////////////////

                    // DEFINIMOS EL Content (CUERPO) CON EL OBJETO A ENVIAR SERIALIZADO.                                   
                    request.Content = new StringContent(JsonConvert.SerializeObject(activo, Formatting.Indented));

                    // DEFINIMOS EL ContentType (CUERPO), EN ESTE CASO ES "application/json"                    
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    // DEFINIMOS EL MÉTODO HTTP
                    request.Method = new HttpMethod("POST");

                    // DEFINIMOS LA URI
                    request.RequestUri = new Uri(url, System.UriKind.RelativeOrAbsolute);

                    /////////////////////////////////////////
                    // CONSTRUIMOS LA RESPUESTA (RESPONSE) //
                    /////////////////////////////////////////

                    // Utilizamos ConfigureAwait(false) para evitar el DeadLock.
                    var respuesta = await _httpclient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                    // OBTENEMOS EL Content DEL RESPONSE como un String
                    // Utilizamos ConfigureAwait(false) para evitar el DeadLock.
                    var responseText_ = await respuesta.Content.ReadAsStringAsync().ConfigureAwait(false);

                    // DESERIALIZAMOS Content DEL RESPONSE                       
                    var responsejson = JsonConvert.DeserializeObject(responseText_, new JsonSerializerSettings { Formatting = Formatting.None });

                    RespuestaApi responseBody_ = new RespuestaApi
                    {
                        HttpStatus = respuesta.StatusCode,
                        JsonResultado = (responsejson != null) ? responsejson.ToString() : string.Empty
                    };
                    return responseBody_;
                }
            }
            finally
            {

            }
        }

        /// <summary>
        /// Realiza la conexión al api, metodo actualizar un activo.
        /// </summary>
        /// <param name="activosUpdate"></param>
        /// <returns></returns>
        public async Task<RespuestaApi> ActivosUpdate(ActivosUpdate activosUpdate)
        {
            // CONSTRUIMOS LA URL DE LA ACCIÓN
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append(_httpclient.BaseAddress != null ? _httpclient.BaseAddress.AbsoluteUri.TrimEnd('/') : "")
                       .Append("/api/activos/")
                       .Append(activosUpdate.IdActivo);
            var url = urlBuilder_.ToString();
            try
            {
                using (var request = new HttpRequestMessage())
                {
                    ///////////////////////////////////////
                    // CONSTRUIMOS LA PETICIÓN (REQUEST) //
                    ///////////////////////////////////////

                    // DEFINIMOS EL Content (CUERPO) CON EL OBJETO A ENVIAR SERIALIZADO.                                   
                    request.Content = new StringContent(JsonConvert.SerializeObject(activosUpdate, Formatting.Indented));

                    // DEFINIMOS EL ContentType (CUERPO), EN ESTE CASO ES "application/json"                    
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    // DEFINIMOS EL MÉTODO HTTP
                    request.Method = new HttpMethod("PUT");

                    // DEFINIMOS LA URI
                    request.RequestUri = new Uri(url, System.UriKind.RelativeOrAbsolute);                    

                    /////////////////////////////////////////
                    // CONSTRUIMOS LA RESPUESTA (RESPONSE) //
                    /////////////////////////////////////////

                    // Utilizamos ConfigureAwait(false) para evitar el DeadLock.
                    var respuesta = await _httpclient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                    // OBTENEMOS EL Content DEL RESPONSE como un String
                    // Utilizamos ConfigureAwait(false) para evitar el DeadLock.
                    var responseText_ = await respuesta.Content.ReadAsStringAsync().ConfigureAwait(false);

                    // DESERIALIZAMOS Content DEL RESPONSE                       
                    var responsejson = JsonConvert.DeserializeObject(responseText_, new JsonSerializerSettings { Formatting = Formatting.None });

                    RespuestaApi responseBody_ = new RespuestaApi
                    {
                        HttpStatus = respuesta.StatusCode,
                        JsonResultado = (responsejson != null) ? responsejson.ToString() : string.Empty
                    };
                    return responseBody_;
                }
            }
            finally
            {

            }
        }

        /// <summary>
        /// Realiza la conexión al api, realiza busqueda personalizada segun parametros enviados.
        /// </summary>
        /// <param name="activosBusquedaModel"></param>
        /// <returns></returns>
        public async Task<RespuestaApi> ActivosBusqueda(ActivosBusquedaModel activosBusquedaModel)
        {
            // CONSTRUIMOS LA URL DE LA ACCIÓN
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append(_httpclient.BaseAddress != null ? _httpclient.BaseAddress.AbsoluteUri.TrimEnd('/') : "")
                       .Append("/api/activos/busqueda");
            var url = urlBuilder_.ToString();
            try
            {
                using (var request = new HttpRequestMessage())
                {
                    ///////////////////////////////////////
                    // CONSTRUIMOS LA PETICIÓN (REQUEST) //
                    ///////////////////////////////////////

                    // DEFINIMOS EL Content (CUERPO) CON EL OBJETO A ENVIAR SERIALIZADO.                                   
                    request.Content = new StringContent(JsonConvert.SerializeObject(activosBusquedaModel, Formatting.Indented));

                    // DEFINIMOS EL ContentType (CUERPO), EN ESTE CASO ES "application/json"                    
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    // DEFINIMOS EL MÉTODO HTTP
                    request.Method = new HttpMethod("GET");

                    // DEFINIMOS LA URI
                    request.RequestUri = new Uri(url, System.UriKind.RelativeOrAbsolute);

                    /////////////////////////////////////////
                    // CONSTRUIMOS LA RESPUESTA (RESPONSE) //
                    /////////////////////////////////////////

                    // Utilizamos ConfigureAwait(false) para evitar el DeadLock.
                    var respuesta = await _httpclient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                    // OBTENEMOS EL Content DEL RESPONSE como un String
                    // Utilizamos ConfigureAwait(false) para evitar el DeadLock.
                    var responseText_ = await respuesta.Content.ReadAsStringAsync().ConfigureAwait(false);

                    // DESERIALIZAMOS Content DEL RESPONSE                       
                    var responsejson = JsonConvert.DeserializeObject(responseText_, new JsonSerializerSettings { Formatting = Formatting.None });

                    RespuestaApi responseBody_ = new RespuestaApi
                    {
                        HttpStatus = respuesta.StatusCode,
                        JsonResultado = (responsejson != null) ? responsejson.ToString() : string.Empty
                    };
                    return responseBody_;
                }
            }
            finally
            {

            }
        }
    }
}
