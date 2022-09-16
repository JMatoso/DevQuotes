using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DevQuotes.Extensions;

public static class HttpContextExtension
{
    public static string GetDataFromHeader(this HttpContext context, string headerKey)
    {
        string value = context.Response.Headers[headerKey];
        return string.IsNullOrEmpty(value) ? string.Empty : value;
    }

    public static void SetDataToHeader<TData>(this HttpContext context, string headerKey, object data)
    {
        context.Response.Headers[headerKey] = JsonConvert.SerializeObject((TData)data);
    }
}
