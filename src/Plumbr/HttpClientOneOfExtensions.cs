using System.Text.Json;
using PlumbR;

namespace System.Net.Http.Json
{
    public static class HttpClientOneOfExtensions
    {
        public static async Task<HttpResult<TResult?>> ReadAsOneOfAsync<TResult>(this HttpResponseMessage response, JsonSerializerOptions? options, CancellationToken cancellationToken = default)
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TResult>(options, cancellationToken);
            }
            else
            {
                return response;
            }
        }

        public static async Task<HttpResultOrError<TResult?, TError>> ReadAsOneOfAsync<TResult, TError>(this HttpResponseMessage response, JsonSerializerOptions? options, CancellationToken cancellationToken = default)
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TResult>(options, cancellationToken);
            }
            else
            {
                return new HttpError<TError>
                {
                    Response = response,
                    ErrorBody = await response.Content.ReadFromJsonAsync<TError>(options, cancellationToken)
                };
            }
        }
    }
}