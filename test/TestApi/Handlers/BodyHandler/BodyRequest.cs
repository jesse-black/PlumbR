namespace PlumbR.TestApi.Handlers.BodyHandler
{
    public class BodyRequest : IPipelineRequest<BodyResult>
    {
        public required int Id { get; init; }
        public required string Name { get; set; }
    }
}