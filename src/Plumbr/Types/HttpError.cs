namespace PlumbR
{
    public class HttpError<TError>
    {
        public required HttpResponseMessage Response { get; init; }
        public required TError? ErrorBody { get; init; }
    }
}