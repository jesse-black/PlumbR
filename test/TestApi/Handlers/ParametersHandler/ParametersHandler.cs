
namespace PlumbR.TestApi.Handlers.ParametersHandler
{
    public class ParametersHandler : IPipelineHandler<ParameterRequest, ParametersResult>
    {
        public async Task<PipelineResult<ParametersResult>> Handle(ParameterRequest request, CancellationToken cancellationToken)
        {
            return new ParametersResult
            {
                Message = $"Your ID {request.Id} has tag {request.Tag}."
            };
        }
    }
}