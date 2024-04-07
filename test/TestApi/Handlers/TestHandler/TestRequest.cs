namespace PlumbR.TestApi.Handlers.TestHandler
{
    public class TestRequest : IPipelineRequest<TestResult>
    {
        public required int Id { get; init; }
        public required string Name { get; set; }
    }
}