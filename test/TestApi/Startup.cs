using FluentValidation;
using PlumbR.TestApi.Handlers.ParametersHandler;
using PlumbR.TestApi.Handlers.TestHandler;

namespace PlumbR.TestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddRouting();
            services.AddAuthorization();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<Startup>();
                cfg.AddValidationBehaviorForAssemblyContaining<Startup>();
            });
            services.AddValidatorsFromAssemblyContaining<Startup>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (app.ApplicationServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment()) // Update the code to use the GetRequiredService method
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/parameters/{Id:int}", Pipeline.HandleParameters<ParameterRequest, ParametersResult>);
                endpoints.MapPost("/body", Pipeline.HandleBody<TestRequest, TestResult>);
            });
        }
    }
}
