
using NexusPay.Api.Infratructures.Observability;
using NexusPay.Api.Middlewares;

namespace NexusPay.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSingleton<HttpTraceQueue>();
            builder.Services.AddHostedService<HttpTraceProcessorService>();
            builder.Services.AddScoped<IObservabilityTool, ObservabilityTool>();
            builder.Services.AddScoped<IHttpTraceRepository, InMemoryHttpTraceRepository>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "NexusPay API v1");
                    options.RoutePrefix = "swagger";
                });
            }

            app.UseMiddleware<RequestTimeLogging>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
