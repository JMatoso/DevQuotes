using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DevQuotes.Extensions.Services.Client.Extensions;

public static class HttpClientExtension
{
    public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T data, string token = "")
    {
        if (!string.IsNullOrEmpty(token)) httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var dataAsString = JsonConvert.SerializeObject(data);
        var content = new StringContent(dataAsString, System.Text.Encoding.UTF8);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return httpClient.PostAsync(url, content);
    }

    public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data, string token = "")
    {
        if (!string.IsNullOrEmpty(token)) httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var dataAsString = JsonConvert.SerializeObject(data);
        var content = new StringContent(dataAsString, System.Text.Encoding.UTF8);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return httpClient.PutAsync(url, content);
    }

    public static Task<HttpResponseMessage> DeleteAs(this HttpClient httpClient, string url, string token)
    {
        if (!string.IsNullOrEmpty(token)) httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return httpClient.DeleteAsync(url);
    }

    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            //throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");
            return default;
        }

        var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        return JsonConvert.DeserializeObject<T>(dataAsString);
    }
}
