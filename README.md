# PlumbR
PlumbR is a minimal api microservices framework that leverages
[MediatR](https://github.com/jbogard/MediatR) to provide handlers for [ASP.NET
Core Minimal
APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-8.0),
using [FluentValidation](https://github.com/FluentValidation/FluentValidation)
to validate the MediatR requests, along with
[OneOf](https://github.com/mcintyre321/OneOf) discriminated unions to allow the
pipelines to return success values or problem details from the handlers.

## Installation

Install the package via NuGet:

```bash
dotnet add package PlumbR
```

## Configuration
To set up PlumbR, first configure MediatR and FluentValidation as usual, then
add `cfg.AddValidationBehaviorForAssemblyContaining<Startup>()` to `AddMediatR`
to wire up the behavior that runs FluentValidation validators before the
pipeline handlers.
```csharp
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Startup>();
    cfg.AddValidationBehaviorForAssemblyContaining<Startup>();
});
services.AddValidatorsFromAssemblyContaining<Startup>();
```
To add other behaviors implementing `IPipelineBehavior<TRequest,
PipelineResult<TResponse>>`, you can use
`AddPipelineBehaviorForAssemblyContaining<T>()`. Normally MediatR's
`AddOpenBehavior` would be used here, but it has trouble wiring up open
behaviors when the result is another generic type like
`PipelineRequest<TResponse>`.
```csharp
services.AddMediatR(cfg =>
{
    // ...
    cfg.AddPipelineBehaviorForAssemblyContaining<Startup>(typeof(LoggerBehavior<>))
});
```

## Usage
### Handler, Request, and Result
Request and Handler classes use `IPipelineRequest<TResult>` and
`IPipelineHandler<TRequest, TResult>` interfaces to match up with the
validators. the response `PipelineResult<TResult>` can be one of `TResult` or
`ProblemDetails`.
```csharp
public class BodyRequest : IPipelineRequest<BodyResult>
{
    public required int Id { get; init; }
    public required string Name { get; set; }
}
public class BodyResult
{
    public required string Message { get; init; }
}
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
```

### Validation
Write validators as you normally would on the request types.
```csharp
public class BodyRequestValidator : AbstractValidator<BodyRequest>
{
    public BodyRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
```

### Endpoints
* Pass `Pipeline.HandleBody<TRequest, TResult>` as the delegate to the endpoint
  mapping to bind the request using `[FromBody]`.
* Pass `Pipeline.HandleParameters<TRequest, TResult>` as the delegate to bind
  the request using `[AsParameters]`. This will allow binding each property on
  the request model from different sources including `[FromRoute]`,
  `[FromQuery]`, `[FromBody]`, etc.
```csharp
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/parameters/{Id:int}", Pipeline.HandleParameters<ParameterRequest, ParametersResult>);
    endpoints.MapPost("/body", Pipeline.HandleBody<BodyRequest, BodyResult>);
});
```

## API Sample
See the [TestApi](https://github.com/jesse-black/PlumbR/tree/main/test/TestApi)
project for a full sample API.

## License
This project is licensed under the Apache License Version 2.0 - see the
[LICENSE](https://github.com/jesse-black/PlumbR/blob/main/LICENSE) file for
details.

