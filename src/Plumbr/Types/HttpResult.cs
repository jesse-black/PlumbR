using OneOf;

namespace PlumbR
{
    [GenerateOneOf]
    public partial class HttpResult<TResult> : OneOfBase<TResult, HttpResponseMessage> { }
}