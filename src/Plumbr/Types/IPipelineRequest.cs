using MediatR;

namespace PlumbR;

/// Represents a pipeline request that returns a response of type <typeparamref name="TResponse"/>.
/// </summary>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface IPipelineRequest<TResponse> : IRequest<PipelineResult<TResponse>> { }