using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlumbR
{
    public static class Pipeline
    {
        public static async Task<IResult> Handle<TRequest, TResponse>(IMediator mediator, TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IRequest<PipelineResult<TResponse>>
        {
            var result = await mediator.Send(request, cancellationToken);
            return result.Match(
                success => Results.Ok(success),
                invalid => Results.BadRequest(new ValidationProblemDetails(invalid.ToDictionary())),
                problem => Results.Problem(problem)
            );
        }

        public static Task<IResult> HandleBody<TRequest, TResponse>(IMediator mediator, [FromBody]TRequest request, CancellationToken cancellationToken)
            where TRequest : IRequest<PipelineResult<TResponse>>
        {
            return Handle<TRequest, TResponse>(mediator, request, cancellationToken);
        }

        public static Task<IResult> HandleQuery<TRequest, TResponse>(IMediator mediator, [FromQuery]TRequest request, CancellationToken cancellationToken)
            where TRequest : IRequest<PipelineResult<TResponse>>
        {
            return Handle<TRequest, TResponse>(mediator, request, cancellationToken);
        }

        public static Task<IResult> HandleRoute<TRequest, TResponse>(IMediator mediator, [FromRoute]TRequest request, CancellationToken cancellationToken)
            where TRequest : IRequest<PipelineResult<TResponse>>
        {
            return Handle<TRequest, TResponse>(mediator, request, cancellationToken);
        }
    }
}