using MediatR;

namespace PlumbR
{
    public interface IPipelineHandler<TRequest, TResult> : IRequestHandler<TRequest, PipelineResult<TResult>>
    where TRequest : IRequest<PipelineResult<TResult>> { }
}