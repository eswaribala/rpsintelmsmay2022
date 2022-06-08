using Camunda.Worker;
using Camunda.Worker.Client;
using CamundaMoviePublisherAPI.Bpmns;
using CamundaMoviePublisherAPI.Handlers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(options =>
             options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Camunda worker startUp
builder.Services.AddSingleton(_ => new BpmnService(configuration["RestApiUri"]));
builder.Services.AddHostedService<BpmnDeployService>();
builder.Services.AddExternalTaskClient()
    .ConfigureHttpClient((provider, client) =>
    {
        client.BaseAddress = new Uri(configuration["RestApiUri"]);
    });
builder.Services.AddCamundaWorker("Movie Data Worker", 1)
    .AddHandler<MovieTaskHandler>()
    .AddHandler<KafkaTaskHandler>();
 
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
