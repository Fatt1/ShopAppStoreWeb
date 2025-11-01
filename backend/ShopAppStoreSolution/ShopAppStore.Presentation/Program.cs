
using Serilog;
using ShopAppStore.Application.Dependency_Injection;
using ShopAppStore.Infrastructure.Dependency_Injection;
using System.Text.Json;

namespace ShopAppStore.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotNetEnv.Env.Load();
            var builder = WebApplication.CreateBuilder(args);
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console().CreateLogger();

            builder.Host.UseSerilog();
            // Add services to the container.
            builder.Services.AddInfrastureService(builder.Configuration);

            builder.Services.AddApplicationService();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            try
            {

                var app = builder.Build();
                app.UseSerilogRequestLogging();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                Log.Logger.Information("Application Starting up");
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
    }
}
