using MediatR;

namespace PlumbR
{
    /// <summary>
    /// Represents a pipeline handler that processes a request and returns a pipeline result.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResult">The type of the pipeline result.</typeparam>
    public interface IPipelineHandler<TRequest, TResult> : IRequestHandler<TRequest, PipelineResult<TResult>>
    where TRequest : IRequest<PipelineResult<TResult>> { }
}