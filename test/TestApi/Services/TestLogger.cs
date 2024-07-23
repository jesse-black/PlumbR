namespace PlumbR.TestApi.Services;

public class TestLogger<T>
{
  private readonly ILogger<T> logger;

  public TestLogger(ILogger<T> logger)
  {
    this.logger = logger;
  }

  public virtual void LogRequest<TRequest>(TRequest request)
  {
    logger.LogInformation("Handling request: {request}", request);
  }

  public virtual void LogResult<TResult>(TResult result)
  {
    logger.LogInformation("Handled request with result: {response}", result);
  }
}