
using Microsoft.AspNetCore.Mvc;

namespace PlumbR.TestApi.Handlers;

public class ParametersHandler : IPipelineHandler<ParameterRequest, ParametersResult>
{
  private readonly TestService service;

  public ParametersHandler(TestService service) {
    this.service = service;
  }

  public async Task<PipelineResult<ParametersResult>> Handle(ParameterRequest request, CancellationToken cancellationToken)
  {
    await service.SaveTag(request.Id, request.Tag, cancellationToken);
    return new ParametersResult
    {
      Message = $"Your ID {request.Id} with tag {request.Tag} was saved."
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