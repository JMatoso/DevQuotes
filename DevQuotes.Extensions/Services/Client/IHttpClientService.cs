namespace DevQuotes.Extensions.Services.Client;

public interface IHttpClientService<T> where T : class
{
    Task<HttpResponseMessage> DeleteAsync(string url, string token = "");
    Task<T> GetAsync(string url, string token = "");
    Task<IEnumerable<T>> GetListAsync(string url, string token = "");
    Task<HttpResponseMessage> PostAsync(T data, string url, string token = "");
    Task<HttpResponseMessage> SendAsync(string url, string token = "");
    Task<HttpResponseMessage> UpdateAsync(T data, string url, string token = "");
}
