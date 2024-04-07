using OneOf;

namespace PlumbR
{
    /// <summary>
    /// Represents a type that can hold either a successful HTTP result or an HTTP error.
    /// </summary>
    /// <typeparam name="TResult">The type of the successful HTTP result body.</typeparam>
    /// <typeparam name="TError">The type of the HTTP error body.</typeparam>
    [GenerateOneOf]
    public partial class HttpResultOrError<TResult, TError> : OneOfBase<TResult, HttpError<TError>> { }
}