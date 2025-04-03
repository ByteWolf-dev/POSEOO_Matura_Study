namespace Base.Web;

using Microsoft.AspNetCore.Http;

public static class HttpRequestExtensions
{
    public static string GetCurrentUri(this HttpRequest request)
    {
        return $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
    }
}