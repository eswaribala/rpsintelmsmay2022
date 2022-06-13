using Jaeger;
using Jaeger.Samplers;
using OpenTracing;
using OpenTracing.Util;
using OrderAPI.Services;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Steeltoe.Discovery.Client;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile(
        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
        optional: true)
    .Build();
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()

    .WriteTo.Debug()
    .WriteTo.Console()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new 
        Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"IntelOrderIndex-{DateTime.UtcNow:yyyy-MM}"
    })
    .Enrich.WithProperty("Environment", environment)
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
//builder.Services.AddDiscoveryClient(configuration);

builder.Services.AddScoped<IPollyInvoker, PollyInvoker>();
builder.Services.AddHttpClient<IPollyInvoker, PollyInvoker>(options =>
{
    options.BaseAddress = new Uri("http://localhost:6060/api/v2/Catalog");
}).AddPolicyHandler(PollyInvoker.GetCircuitBreakerPolicy())
.AddPolicyHandler(PollyInvoker.GetRetryPolicy());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITracer>(serviceProvider =>
{
    string serviceName = Assembly.GetEntryAssembly().GetName().Name;

    ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

    ISampler sampler = new ConstSampler(sample: true);

    ITracer tracer = new Tracer.Builder(serviceName)
        .WithLoggerFactory(loggerFactory)
        .WithSampler(sampler)
        .Build();

    GlobalTracer.Register(tracer);

    return tracer;
});

builder.Services.AddOpenTracing();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
