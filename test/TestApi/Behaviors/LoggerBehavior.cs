using MediatR;
using PlumbR.TestApi.Services;

namespace PlumbR.TestApi;

public class LoggerBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, PipelineResult<TResult>>
  where TRequest : notnull
{
  private readonly TestLogger<LoggerBehavior<TRequest, TResult>> logger;

  public LoggerBehavior(TestLogger<LoggerBehavior<TRequest, TResult>> logger)
  {
    this.logger = logger;
  }

  public async Task<PipelineResult<TResult>> Handle(TRequest request, RequestHandlerDelegate<PipelineResult<TResult>> next, CancellationToken cancellationToken)
  {
    logger.LogRequest(request);
    var result = await next();
    logger.LogResult(result.Value);
    return result;
  }
}