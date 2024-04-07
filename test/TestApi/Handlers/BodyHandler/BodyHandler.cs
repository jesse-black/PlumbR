namespace PlumbR.TestApi.Handlers.BodyHandler
{
    public class BodyHandler : IPipelineHandler<BodyRequest, BodyResult>
    {
        public async Task<PipelineResult<BodyResult>> Handle(BodyRequest request, CancellationToken cancellationToken)
        {
            return new BodyResult
            {
                Message = $"Hello, {request.Name}! Your ID is {request.Id}."
            };
        }
    }
}