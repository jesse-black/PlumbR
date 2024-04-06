using OneOf;

namespace PlumbR
{
    [GenerateOneOf]
    public partial class HttpResultOrError<TResult, TError> : OneOfBase<TResult, HttpError<TError>> { }
}