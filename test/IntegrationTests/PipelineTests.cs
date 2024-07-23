using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using PlumbR.TestApi;
using PlumbR.TestApi.Handlers;
using PlumbR.TestApi.Services;

namespace PlumbR.IntegrationTests;

public class PipelineTests : IClassFixture<WebApplicationFactory<Program>>
{
  private TestLogger<LoggerBehavior<BodyRequest, BodyResult>>? mockLogger;
  private readonly WebApplicationFactory<Program> factory;

  public PipelineTests(WebApplicationFactory<Program> factory)
  {
    this.factory = factory.WithWebHostBuilder(builder =>
    {
      builder.UseEnvironment("Test");
      builder.ConfigureServices(services =>
      {
        services.AddScoped<TestLogger<LoggerBehavior<BodyRequest, BodyResult>>>(sp =>{
          var logger = sp.GetRequiredService<ILogger<LoggerBehavior<BodyRequest, BodyResult>>>();
          mockLogger = Substitute.For<TestLogger<LoggerBehavior<BodyRequest, BodyResult>>>(logger);
          return mockLogger;
        });
      });
    });
  }

  [Fact]
  public async Task PipelineBodyEndpoint_ValidRequest_ReturnsResponse()
  {
    // Arrange
    var client = factory.CreateClient();

    // Act
    var response = await client.PostAsJsonAsync("/body", new
    {
      Name = "Test",
      Id = 19
    });

    // Assert
    response.EnsureSuccessStatusCode();
    var content = await response.Content.ReadFromJsonAsync<BodyResult>();
    content.Should().BeEquivalentTo(new BodyResult
    {
      Message = "Hello, Test! Your ID 19 was saved."
    });
  }

  [Fact]
  public async Task PipelineParametersEndpoint_ValidRequest_ReturnsResponse()
  {
    // Arrange
    var client = factory.CreateClient();

    // Act
    var response = await client.GetAsync("/parameters/18?tag=foo");

    // Assert
    response.EnsureSuccessStatusCode();
    var content = await response.Content.ReadFromJsonAsync<BodyResult>();
    content.Should().BeEquivalentTo(new ParametersResult
    {
      Message = "Your ID 18 with tag foo was saved."
    });
  }

  [Fact]
  public async Task PipelineBodyEndpoint_EmptyBody_ReturnsBadRequest()
  {
    // Arrange
    var client = factory.CreateClient();

    // Act
    var response = await client.PostAsJsonAsync("/body", new { });

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
  }

  [Fact]
  public async Task PipelineBodyEndpoint_NegativeId_ReturnsBadRequestProblemDetails()
  {
    // Arrange
    var client = factory.CreateClient();

    // Act
    var response = await client.PostAsJsonAsync("/body", new
    {
      Name = "Test",
      Id = -19
    });

    // Assert
    var content = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    content.Should().BeEquivalentTo(new ValidationProblemDetails(new Dictionary<string, string[]>
    {
        { "Id", new [] { "'Id' must be greater than '0'." } }
    })
    {
      Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
      Status = (int)HttpStatusCode.BadRequest
    });
  }

  [Fact]
  public async Task PipelineBodyEndpoint_NumberNine_ReturnsUnprocessableEntityProblemDetails()
  {
    // Arrange
    var client = factory.CreateClient();

    // Act
    var response = await client.PostAsJsonAsync("/body", new
    {
      Name = "Test",
      Id = 9
    });

    // Assert
    var content = await response.Content.ReadFromJsonAsync<ProblemDetails>();
    response.StatusCode.Should().Be(HttpStatusCode.UnprocessableContent);
    content.Should().BeEquivalentTo(new ProblemDetails()
    {
      Status = StatusCodes.Status422UnprocessableEntity,
      Title = "Failed to save the data.",
      Type = "https://tools.ietf.org/html/rfc4918#section-11.2"
    });
  }

  [Fact]
  public async Task PipelineEndpoint_LogWasCalled()
  {
    // Arrange
    var client = factory.CreateClient();

    // Act
    var response = await client.PostAsJsonAsync("/body", new
    {
      Name = "Test",
      Id = 19
    });

    // Assert
    response.EnsureSuccessStatusCode();
    var content = await response.Content.ReadFromJsonAsync<BodyResult>();
    content.Should().BeEquivalentTo(new BodyResult
    {
      Message = "Hello, Test! Your ID 19 was saved."
    });
    mockLogger.Should().NotBeNull();
    mockLogger!.Received().LogRequest(Arg.Any<BodyRequest>());
    mockLogger!.Received().LogResult(Arg.Any<BodyResult>());
  }
}