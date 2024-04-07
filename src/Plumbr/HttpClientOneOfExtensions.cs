using System.Text.Json;
using PlumbR;

namespace System.Net.Http.Json
{
    public static class HttpClientOneOfExtensions
    {
        /// <summary>
        /// Reads the HTTP response content as either a successful result or the original response, based on the status code of the response.
        /// </summary>
        /// <typeparam name="TResult">The type to deserialize the response content to.</typeparam>
        /// <param name="response">The HTTP response message.</param>
        /// <param name="options">The optional <see cref="JsonSerializerOptions"/> used to configure the deserialization process.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        /// <returns>A OneOf type representing the deserialized response content, or the original response if the status code indicates an error.</returns>
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

        /// <summary>
        /// Reads the HTTP response content as either a successful result or an error, based on the status code of the response.
        /// </summary>
        /// <typeparam name="TResult">The type of the successful result.</typeparam>
        /// <typeparam name="TError">The type of the error.</typeparam>
        /// <param name="response">The HTTP response message.</param>
        /// <param name="options">The JSON serializer options (optional).</param>
        /// <param name="cancellationToken">The cancellation token (optional).</param>
        /// <returns>A OneOf type representing the deserialized content response, or an error object.</returns>
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