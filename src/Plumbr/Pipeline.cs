using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlumbR;

public static class Pipeline
{
  /// <summary>
  /// Handles the request using the mediator and returns the result.
  /// </summary>
  /// <typeparam name="TRequest">The type of the request.</typeparam>
  /// <typeparam name="TResponse">The type of the response.</typeparam>
  /// <param name="mediator">The mediator instance.</param>
  /// <param name="request">The request to handle.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The IResult result of handling the request.</returns>
  public static async Task<IResult> Handle<TRequest, TResponse>(IMediator mediator, TRequest request, CancellationToken cancellationToken = default)
      where TRequest : IRequest<PipelineResult<TResponse>>
  {
    var result = await mediator.Send(request, cancellationToken);
    return result.Match(
        success => Results.Ok(success),
        problem => Results.Problem(problem)
    );
  }

  /// <summary>
  /// Handles the request object from the body of the HTTP request using the mediator and returns the result.
  /// </summary>
  /// <typeparam name="TRequest">The type of the request.</typeparam>
  /// <typeparam name="TResponse">The type of the response.</typeparam>
  /// <param name="mediator">The mediator instance.</param>
  /// <param name="request">The request object.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The IResult result of handling the request.</returns>
  public static Task<IResult> HandleBody<TRequest, TResponse>(IMediator mediator, [FromBody] TRequest request, CancellationToken cancellationToken)
      where TRequest : IRequest<PipelineResult<TResponse>>
  {
    return Handle<TRequest, TResponse>(mediator, request, cancellationToken);
  }

  /// <summary>
  /// Handles the request object as parameters of the HTTP request using the mediator and returns the result.
  /// </summary>
  /// <typeparam name="TRequest">The type of the request.</typeparam>
  /// <typeparam name="TResponse">The type of the response.</typeparam>
  /// <param name="mediator">The mediator instance.</param>
  /// <param name="request">The request object.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The IResult result of handling the request.</returns>        
  public static Task<IResult> HandleParameters<TRequest, TResponse>(IMediator mediator, [AsParameters] TRequest request, CancellationToken cancellationToken)
      where TRequest : IRequest<PipelineResult<TResponse>>
  {
    return Handle<TRequest, TResponse>(mediator, request, cancellationToken);
  }
}