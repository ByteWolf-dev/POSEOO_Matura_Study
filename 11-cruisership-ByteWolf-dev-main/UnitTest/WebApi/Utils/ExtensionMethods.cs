namespace UnitTest.WebApi.Utils
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public static class ExtensionMethods
    {
        public static Task<T> GetAndDeserialize<T>(this HttpClient client, string requestUri)
        {
            return client.GetFromJsonAsync<T>(requestUri);
        }
    }
}