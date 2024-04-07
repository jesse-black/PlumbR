using Microsoft.AspNetCore.Mvc;

namespace PlumbR.TestApi.Handlers.ParametersHandler
{
    public class ParameterRequest : IPipelineRequest<ParametersResult>
    {
        [FromRoute]
        public required int Id { get; init; }
        [FromQuery]
        public required string Tag { get; set; }
    }
}