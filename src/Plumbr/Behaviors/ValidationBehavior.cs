using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PlumbR.Behaviors;

/// <summary>
/// Represents a behavior that performs validation on a request before it is handled by the pipeline.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, PipelineResult<TResponse>>
    where TRequest : IRequest<PipelineResult<TResponse>>
{
  private readonly IEnumerable<IValidator<TRequest>> validators;

  /// <summary>
  /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class.
  /// </summary>
  /// <param name="validators">The validators to be used for validating the request.</param>
  public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
  {
    this.validators = validators;
  }

  /// <summary>
  /// Handles the request by performing validation and invoking the next handler in the pipeline.
  /// </summary>
  /// <param name="request">The request to be handled.</param>
  /// <param name="next">The delegate representing the next handler in the pipeline.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation, containing the result of the pipeline.</returns>
  public async Task<PipelineResult<TResponse>> Handle(
      TRequest request,
      RequestHandlerDelegate<PipelineResult<TResponse>> next,
      CancellationToken cancellationToken)
  {
    var context = new ValidationContext<TRequest>(request);

    var results = await Task.WhenAll(validators.Select(validator => validator.ValidateAsync(context)));
    var result = new ValidationResult(results);

    if (!result.IsValid)
    {
      return new ValidationProblemDetails(result.ToDictionary());
    }

    return await next();
  }
}