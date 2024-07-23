using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PlumbR.TestApi.Services;

namespace PlumbR.TestApi.Handlers;

public class BodyHandler : IPipelineHandler<BodyRequest, BodyResult>
{
  private readonly TestService service;

  public BodyHandler(TestService service)
  {
    this.service = service;
  }

  public async Task<PipelineResult<BodyResult>> Handle(BodyRequest request, CancellationToken cancellationToken)
  {
    try
    {
      await service.SaveId(request.Id, cancellationToken);
    }
    catch (SaveFailedException e)
    {
      return new ProblemDetails
      {
        Status = StatusCodes.Status422UnprocessableEntity,
        Title = e.Message
      };
    }

    return new BodyResult
    {
      Message = $"Hello, {request.Name}! Your ID {request.Id} was saved."
    };
  }
}

public class BodyRequest : IPipelineRequest<BodyResult>
{
  public required int Id { get; init; }
  public required string Name { get; set; }
}

public class BodyResult
{
  public required string Message { get; init; }
}

public class BodyRequestValidator : AbstractValidator<BodyRequest>
{
  public BodyRequestValidator()
  {
    RuleFor(x => x.Id).GreaterThan(0);
  }
}