using System.Net.Http.Json;
using HttpBase.DTO.Archivos;
using HttpBase.DTO.Common;
using HttpBase.Exceptions;
using HttpBase.Filters;
using HttpBase.Interfaces;
using HttpBase.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HttpBase;

[TypeFilter(typeof(ExceptionFilterGeneric))]
public abstract partial class HttpClient_APIBase
{
    private HttpClient _httpClient;
    private HttpResponseMessage resultado;
    private ICurrentUserService _currentUserService;

    public HttpClient_APIBase(HttpClient httpClient, ICurrentUserService currentUserService)
    {
        _httpClient = httpClient;
        _currentUserService = currentUserService;
    }

    public enum SendType : int
    {
        Get_Full = 1,
        Get_Full_Cab = 2,
        Get_Single = 3,
        Post = 4,
        Post_Full = 5,
        Put = 6,
        Delete = 7,
        Delete_Full = 8,
        Get_File = 9,
        Post_File = 10,
    }

    [TypeFilter(typeof(ExceptionFilterGeneric))]
    public async Task<T> SendAsyncCustom<T>(SendType type, string uri, T content = default(T))
        where T : notnull, IBaseDTO, IErrores, new()
    {
        if (_currentUserService.UserToken.Length > 15)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _currentUserService.UserToken);
        }

        HttpRequestMessage request = new();
        var userId = _currentUserService.UserId;

        switch (type)
        {
            case SendType.Get_Single:
                request = new(HttpMethod.Get, uri);
                break;
            case SendType.Post:
                request = new(HttpMethod.Post, uri);
                request.Content = new StringContent(JsonConvert.SerializeObject(content));
                request.Content.Headers.Remove("Content-Type");
                request.Content.Headers.Add("Content-Type", "application/json");
                break;
            case SendType.Post_Full:
                request = new(HttpMethod.Post, uri);
                request.Content = new StringContent(JsonConvert.SerializeObject(content));
                request.Content.Headers.Remove("Content-Type");
                request.Content.Headers.Add("Content-Type", "application/json");
                break;
            case SendType.Post_File:
                request = new(HttpMethod.Post, uri);
                request.Content = new StringContent(JsonConvert.SerializeObject(content));
                request.Content.Headers.Remove("Content-Type");
                request.Content.Headers.Add("Content-Type", "application/json");
                break;
            case SendType.Put:
                request = new(HttpMethod.Put, uri);
                request.Content = new StringContent(JsonConvert.SerializeObject(content));
                request.Content.Headers.Remove("Content-Type");
                request.Content.Headers.Add("Content-Type", "application/json");
                break;
            case SendType.Delete:
                request = new(HttpMethod.Delete, uri);
                request.Content = new StringContent(JsonConvert.SerializeObject(content));
                request.Content.Headers.Remove("Content-Type");
                request.Content.Headers.Add("Content-Type", "application/json");
                break;
            case SendType.Delete_Full:
                request = new(HttpMethod.Delete, uri);
                request.Content = new StringContent(JsonConvert.SerializeObject(content));
                request.Content.Headers.Remove("Content-Type");
                request.Content.Headers.Add("Content-Type", "application/json");
                break;
            default:
                throw new Exception($"El tipo enviado es incorrecto:{type.ToString()}");
        }

        resultado = await _httpClient.SendAsync(request);

        string res = await resultado.Content.ReadAsStringAsync();

        int statuscode = (int)resultado.StatusCode;

        if (Enumerable.Range(300, 300).Contains(statuscode))
        {
            T respuesta = new();
            respuesta.Errores = new();
            try
            {
                respuesta.Errores.Add(await resultado.Content.ReadFromJsonAsync<ProblemDetails>());
            }
            catch (Exception)
            {
                ProblemDetails problem = new()
                {
                    Status = statuscode,
                    Title = $"$(int)resultado.StatusCode).ToString()",
                    Detail = resultado.ReasonPhrase,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                };

                respuesta.Errores.Add(problem);
            }
            return respuesta;
        }

        try
        {
            if (resultado.IsSuccessStatusCode)
            {
                switch (type)
                {
                    case SendType.Get_Single:
                        {
                            Respuesta_Get_Single_Models<T> respuesta = new();
                            respuesta.isError = !resultado.IsSuccessStatusCode;
                            respuesta = await resultado.Content.ReadFromJsonAsync<Respuesta_Get_Single_Models<T>>();

                            return respuesta.resultado;
                        }

                    case SendType.Post:
                        {
                            Respuesta_Post_Models<T> respuesta = new();
                            respuesta.isError = !resultado.IsSuccessStatusCode;
                            respuesta = await resultado.Content.ReadFromJsonAsync<Respuesta_Post_Models<T>>();
                            respuesta.request.Id = respuesta.resultado;

                            return respuesta.request;
                        }

                    case SendType.Post_Full:
                        {
                            Respuesta_Post_Full_Models<T> respuesta = new();
                            respuesta.isError = !resultado.IsSuccessStatusCode;
                            respuesta = await resultado.Content.ReadFromJsonAsync<Respuesta_Post_Full_Models<T>>();

                            return respuesta.request;
                        }

                    case SendType.Post_File:
                        {
                            Respuesta_Post_File_Models<T> respuesta = new();
                            respuesta.isError = !resultado.IsSuccessStatusCode;
                            respuesta = await resultado.Content.ReadFromJsonAsync<Respuesta_Post_File_Models<T>>();

                            return respuesta.request;
                        }

                    case SendType.Put:
                        {
                            Respuesta_Put_Models<T> respuesta = new();
                            respuesta.isError = !resultado.IsSuccessStatusCode;
                            respuesta = await resultado.Content.ReadFromJsonAsync<Respuesta_Put_Models<T>>();

                            return respuesta.request;
                        }

                    case SendType.Delete:
                        {
                            Respuesta_Delete_Models<T> respuesta = new();
                            respuesta.isError = !resultado.IsSuccessStatusCode;
                            respuesta = await resultado.Content.ReadFromJsonAsync<Respuesta_Delete_Models<T>>();
                            respuesta.request.Id = respuesta.resultado;

                            return respuesta.request;
                        }

                    case SendType.Delete_Full:
                        {
                            Respuesta_Delete_Models<T> respuesta = new();
                            respuesta.isError = !resultado.IsSuccessStatusCode;
                            respuesta = await resultado.Content.ReadFromJsonAsync<Respuesta_Delete_Models<T>>();
                            respuesta.request.Id = respuesta.resultado;

                            return respuesta.request;
                        }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception_Deserialize(
                $@"Message: {ex.Message} 
                  Statuscode: {resultado.StatusCode}
                  Resl:  {res}");
        }

        throw new Exception();
    }

    [TypeFilter(typeof(ExceptionFilterGeneric))]
    public async Task<(T Items, int Cant)> SendAsyncCustom<T>(SendType type, string uri)
        where T : notnull, IBaseDTO, IErrores, new()
    {
        if (_currentUserService.UserToken.Length > 15)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _currentUserService.UserToken);
        }

        HttpRequestMessage request = new();

        switch (type)
        {
            case SendType.Get_Full:
                request = new(HttpMethod.Get, uri);
                break;
            default:
                throw new Exception($"El tipo enviado es incorrecto:{type.ToString()}");
        }

        try
        {
            resultado = await _httpClient.SendAsync(request);
        }
        catch (Exception)
        {
            throw;
        }

        string res = await resultado.Content.ReadAsStringAsync();
        int statuscode = (int)resultado.StatusCode;

        if (Enumerable.Range(300, 300).Contains(statuscode))
        {
            T respuesta = new();
            respuesta.Errores = new();
            try
            {
                respuesta.Errores.Add(await resultado.Content.ReadFromJsonAsync<ProblemDetails>());
            }
            catch (Exception)
            {
                ProblemDetails problem = new()
                {
                    Status = statuscode,
                    Title = $"$(int)resultado.StatusCode).ToString()",
                    Detail = resultado.ReasonPhrase,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                };

                respuesta.Errores.Add(problem);
            }

            return (respuesta, statuscode);
        }

        try
        {
            if (resultado.IsSuccessStatusCode)
            {
                switch (type)
                {
                    case SendType.Get_Full:
                        {
                            Respuesta_Get_Full_Models<T> respuesta = new();
                            respuesta.isError = !resultado.IsSuccessStatusCode;
                            respuesta = await resultado.Content.ReadFromJsonAsync<Respuesta_Get_Full_Models<T>>();

                            return (respuesta.resultado.items, respuesta.resultado.totalCount);
                        }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception_Deserialize(
                $@"Message: {ex.Message} 
                  Statuscode: {resultado.StatusCode}
                  Resl:  {res}");
        }

        throw new Exception();
    }

    [TypeFilter(typeof(ExceptionFilterGeneric))]
    public async Task<(T1 Items, T2 Header, int Cant)> SendAsyncCustom<T1, T2>(SendType type, string uri)
        where T1 : notnull, IBaseDTO, IErrores, new()
    {
        if (_currentUserService.UserToken.Length > 15)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _currentUserService.UserToken);
        }

        HttpRequestMessage request = new();

        switch (type)
        {
            case SendType.Get_Full_Cab:
                request = new(HttpMethod.Get, uri);
                break;
            default:
                throw new Exception($"El tipo enviado es incorrecto:{type.ToString()}");
        }

        resultado = await _httpClient.SendAsync(request);

        string res = await resultado.Content.ReadAsStringAsync();
        int statuscode = (int)resultado.StatusCode;

        if (Enumerable.Range(300, 300).Contains(statuscode))
        {
            T1 respuesta = new();
            respuesta.Errores = new();
            try
            {
                respuesta.Errores.Add(await resultado.Content.ReadFromJsonAsync<ProblemDetails>());
            }
            catch (Exception)
            {
                ProblemDetails problem = new()
                {
                    Status = statuscode,
                    Title = $"$(int)resultado.StatusCode).ToString()",
                    Detail = resultado.ReasonPhrase,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                };

                respuesta.Errores.Add(problem);
            }

            return (respuesta, default(T2), 0);
        }

        try
        {
            if (resultado.IsSuccessStatusCode)
            {
                switch (type)
                {
                    case SendType.Get_Full_Cab:
                        {
                            Respuesta_Get_Full_Models<T1, T2> respuesta = new();
                            respuesta.isError = !resultado.IsSuccessStatusCode;
                            respuesta = await resultado.Content.ReadFromJsonAsync<Respuesta_Get_Full_Models<T1, T2>>();

                            return (respuesta.resultado.items, respuesta.resultado.header, respuesta.resultado.totalCount);
                        }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception_Deserialize(
                $@"Message: {ex.Message} 
                  Statuscode: {resultado.StatusCode}
                  Resl:  {res}");
        }

        throw new Exception();
    }

    [TypeFilter(typeof(ExceptionFilterGeneric))]
    public async Task<T> SendAsyncFileCustom<T>(SendType type, string uri, IFormFile img, string fileName = null)
        where T : class, IErrores, new()
    {
        if (_currentUserService.UserToken.Length > 15)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _currentUserService.UserToken);
        }

        HttpRequestMessage request = new();

        switch (type)
        {
            case SendType.Get_File:
                request = new(HttpMethod.Get, uri);
                request.Content = new StringContent(string.Empty);
                request.Content.Headers.Remove("Content-Type");
                request.Content.Headers.Add("Content-Type", "application/json");
                break;

            case SendType.Post_File:
                request = new(HttpMethod.Post, uri);
                request.Content = new StringContent(JsonConvert.SerializeObject(img));
                request.Content.Headers.Remove("Content-Type");
                request.Content.Headers.Add("Content-Type", "multipart/form-data");

                var multipartContent = new MultipartFormDataContent();
                using (var ms = new MemoryStream())
                {
                    fileName ??= img.FileName;
                    img.CopyTo(ms);

                    var bytes = ms.ToArray();

                    ByteArrayContent bytes_array_content = new ByteArrayContent(bytes);
                    multipartContent.Add(bytes_array_content, "File", fileName);
                    request.Content = multipartContent;
                }

                break;

            default:
                throw new Exception($"El tipo enviado es incorrecto:{type.ToString()}");
        }

        resultado = await _httpClient.SendAsync(request);

        string res = await resultado.Content.ReadAsStringAsync();
        int statuscode = (int)resultado.StatusCode;

        if (Enumerable.Range(300, 300).Contains(statuscode))
        {
            T respuesta = new();
            respuesta.Errores = new();
            try
            {
                respuesta.Errores.Add(await resultado.Content.ReadFromJsonAsync<ProblemDetails>());
            }
            catch (Exception)
            {
                ProblemDetails problem = new()
                {
                    Status = statuscode,
                    Title = $"$(int)resultado.StatusCode).ToString()",
                    Detail = resultado.ReasonPhrase,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                };
                respuesta.Errores.Add(problem);
            }

            return respuesta;
        }

        if (Enumerable.Range(500, 599).Contains((int)resultado.StatusCode))
            throw new Exception_API($"Motivo:{resultado.ReasonPhrase} URL:{request.RequestUri}", (int)resultado.StatusCode);

        try
        {
            if (resultado.IsSuccessStatusCode)
            {
                switch (type)
                {
                    case SendType.Post_File:
                        {
                            Respuesta_Post_File<T> respuesta = new();
                            respuesta = await resultado.Content.ReadFromJsonAsync<Respuesta_Post_File<T>>();

                            return respuesta.resultado;
                        }

                    case SendType.Get_File:
                        {
                            FileGetDTO respuest = new();
                            respuest.File = await resultado.Content.ReadAsStreamAsync();

                            return respuest as T;
                        }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception_Deserialize(
                $@"Message: {ex.Message} 
                  Statuscode: {resultado.StatusCode}
                  Resl:  {res}");
        }

        throw new Exception();
    }
}
