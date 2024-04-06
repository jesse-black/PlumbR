using MediatR;

namespace PlumbR
{
    public interface IPipelineRequest<TResponse> : IRequest<PipelineResult<TResponse>> { }
}