using CatalogAPI.Contexts;
using CatalogAPI.Models;
using CatalogAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
Dictionary<String, Object> data = new VaultConfiguration().GetDBCredentials().Result;
Console.WriteLine(data);
SqlConnectionStringBuilder providerCs = new SqlConnectionStringBuilder();
providerCs.InitialCatalog = data["dbname"].ToString();
providerCs.UserID = data["username"].ToString();
providerCs.Password = data["password"].ToString();
providerCs.DataSource = data["servername"].ToString();

//providerCs.UserID = CryptoService2.Decrypt(ConfigurationManager.AppSettings["UserId"]);
providerCs.MultipleActiveResultSets = true;
providerCs.TrustServerCertificate = false;

builder.Services.AddDbContext<CatalogContext>(o => o.UseSqlServer(providerCs.ToString()));




// Add services to the container.
//builder.Services.AddDbContext<CatalogContext>(options => options
//.UseSqlServer(configuration.GetConnectionString("Catalog_Conn_String")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddTransient<ICatalogRepository,CatalogRepository>();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddApiVersioning();
builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1, 0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
    x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
});
builder.Services.AddSwaggerGen();

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
