
using Microsoft.AspNetCore.Mvc;

namespace PlumbR.TestApi.Handlers;

public class ParametersHandler : IPipelineHandler<ParameterRequest, ParametersResult>
{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
  public async Task<PipelineResult<ParametersResult>> Handle(ParameterRequest request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
  {
    return new ParametersResult
    {
      Message = $"Your ID {request.Id} has tag {request.Tag}."
    };
  }
}

public class ParameterRequest : IPipelineRequest<ParametersResult>
{
  [FromRoute]
  public required int Id { get; init; }
  [FromQuery]
  public required string Tag { get; set; }
}

public class ParametersResult
{
  public required string Message { get; init; }
}