using DevQuotes.Extensions.Services.Client.Extensions;
using System.Net.Http.Headers;

namespace DevQuotes.Extensions.Services.Client;

public class HttpClientService<T> : IHttpClientService<T> where T : class
{
    private readonly HttpClient _httpClient;

    public HttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private void SetAuthorization(string token)
    {
        if (!string.IsNullOrEmpty(token))
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    }

    public async Task<IEnumerable<T>> GetListAsync(string url, string token = "")
    {
        SetAuthorization(token);

        var response = await _httpClient.GetAsync(url);

        return await response.ReadContentAs<IEnumerable<T>>(); ;
    }

    public async Task<T> GetAsync(string url, string token = "")
    {
        SetAuthorization(token);

        var response = await _httpClient.GetAsync(url);
        return await response.ReadContentAs<T>();
    }

    public async Task<HttpResponseMessage> SendAsync(string url, string token = "")
    {
        SetAuthorization(token);

        return await _httpClient.GetAsync(url);
    }
    public async Task<HttpResponseMessage> PostAsync(T data, string url, string token = "")
    {
        SetAuthorization(token);

        return await _httpClient.PostAsJson(url, data);
    }
    public async Task<HttpResponseMessage> UpdateAsync(T data, string url, string token = "")
    {
        SetAuthorization(token);

        return await _httpClient.PutAsJson(url, data);
    }
    public async Task<HttpResponseMessage> DeleteAsync(string url, string token = "")
    {
        SetAuthorization(token);

        return await _httpClient.DeleteAsync(url);
    }
}

