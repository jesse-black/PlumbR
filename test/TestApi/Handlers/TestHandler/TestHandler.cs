namespace PlumbR.TestApi.Handlers.TestHandler
{
    public class TestHandler : IPipelineHandler<TestRequest, TestResult>
    {
        public async Task<PipelineResult<TestResult>> Handle(TestRequest request, CancellationToken cancellationToken)
        {
            return new TestResult
            {
                Message = $"Hello, {request.Name}! Your ID is {request.Id}."
            };
        }
    }
}