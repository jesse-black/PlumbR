using FluentValidation;

namespace PlumbR.TestApi.Handlers;

public class BodyHandler : IPipelineHandler<BodyRequest, BodyResult>
{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
  public async Task<PipelineResult<BodyResult>> Handle(BodyRequest request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
  {
    return new BodyResult
    {
      Message = $"Hello, {request.Name}! Your ID is {request.Id}."
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