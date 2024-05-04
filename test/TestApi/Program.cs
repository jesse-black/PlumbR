using FluentValidation;
using PlumbR;
using PlumbR.TestApi;
using PlumbR.TestApi.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting();
builder.Services.AddProblemDetails();
builder.Services.AddMediatR(cfg =>
{
  cfg.RegisterServicesFromAssemblyContaining<Program>();
  cfg.AddValidationBehaviorForAssemblyContaining<Program>();
});
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddScoped<TestService>();

var app = builder.Build();

app.MapGet("/parameters/{Id:int}", Pipeline.HandleParameters<ParameterRequest, ParametersResult>);
app.MapPost("/body", Pipeline.HandleBody<BodyRequest, BodyResult>);

app.Run();

public partial class Program { }