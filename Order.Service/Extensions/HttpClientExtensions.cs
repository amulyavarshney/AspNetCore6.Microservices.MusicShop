using System.Text.Json;

namespace Order.Service.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> GetAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Exception calling the API : {response.ReasonPhrase}");
            }
            var dataString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(dataString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}