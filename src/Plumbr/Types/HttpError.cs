namespace PlumbR
{
    /// <summary>
    /// Represents an HTTP error response with an associated error body.
    /// </summary>
    /// <typeparam name="TError">The type of the error body.</typeparam>
    public class HttpError<TError>
    {
        /// <summary>
        /// Gets or sets the HTTP response associated with the error.
        /// </summary>
        public required HttpResponseMessage Response { get; init; }

        /// <summary>
        /// Gets or sets the error body associated with the HTTP error response.
        /// </summary>
        public TError? ErrorBody { get; init; }
    }
}