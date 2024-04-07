using OneOf;

namespace PlumbR
{
    /// <summary>
    /// Represents an HTTP result that can either contain a result of type <typeparamref name="TResult"/> or an <see cref="HttpResponseMessage"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    [GenerateOneOf]
    public partial class HttpResult<TResult> : OneOfBase<TResult, HttpResponseMessage> { }
}