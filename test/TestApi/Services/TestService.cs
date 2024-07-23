namespace PlumbR.TestApi.Services;

public class TestService
{
  public async Task SaveId(int id, CancellationToken cancellationToken)
  {
    // Simulate async work with Task.Delay
    await Task.Delay(0, cancellationToken);
    if (id == 9)
    {
      throw new SaveFailedException();
    }
  }

  public async Task SaveTag(int id, string tag, CancellationToken cancellationToken)
  {
    // Simulate async work with Task.Delay
    await Task.Delay(0, cancellationToken);
    if (id == 9)
    {
      throw new SaveFailedException();
    }
  }
}

public class SaveFailedException : Exception
{
  public SaveFailedException() : base("Failed to save the data.") { }
}