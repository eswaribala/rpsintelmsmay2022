using CatalogAPI.Contexts;
using CatalogAPI.Models;
using CatalogAPI.Repositories;
using CatalogAPI.Schemas;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.Discovery.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddConfigServer();

//External Configuration
ConfigurationManager configuration = builder.Configuration;
Dictionary<String, Object> data = new VaultConfiguration(configuration).GetDBCredentials().Result;
Console.WriteLine(data);
SqlConnectionStringBuilder providerCs = new SqlConnectionStringBuilder();
providerCs.InitialCatalog = data["dbname"].ToString();
providerCs.UserID = data["username"].ToString();
providerCs.Password = data["password"].ToString();
providerCs.DataSource = "DESKTOP-55AGI0I\\MSSQLEXPRESS2021";

//providerCs.UserID = CryptoService2.Decrypt(ConfigurationManager.AppSettings["UserId"]);
providerCs.MultipleActiveResultSets = true;
providerCs.TrustServerCertificate = false;

builder.Services.AddDbContext<CatalogContext>(o => o.UseSqlServer(providerCs.ToString()));




// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options
.UseSqlServer(configuration.GetConnectionString("Identity_Conn_String")));

builder.Services.AddControllers();
builder.Services.AddDiscoveryClient(configuration);
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

builder.Services.AddScoped<CatalogSchema>();
builder.Services.AddGraphQL()
   .AddSystemTextJson()
   .AddGraphTypes(typeof(CatalogSchema), ServiceLifetime.Scoped);

// For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseGraphQL<CatalogSchema>();
app.UseGraphQLPlayground(options: new PlaygroundOptions());

app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder
    .WithOrigins("http://localhost:4200", "http://localhost:3000", "http://localhost:7072")
    .WithMethods("PUT", "DELETE", "GET", "POST")
    .AllowAnyHeader();


});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
