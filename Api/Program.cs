using Api;
using GlobalUtility;
using GlobalUtility.Configuration;
using GlobalUtility.Middleware;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using UserModule;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string ASPNETCORE_ENVIRONMENT = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLower() ?? "production";
IConfiguration _config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{ASPNETCORE_ENVIRONMENT}.json", true)
    .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
    .Build();

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        opt.JsonSerializerOptions.AllowTrailingCommas = true;
        opt.JsonSerializerOptions.WriteIndented = true;
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
        opt.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// mapper configuration
//builder.Configuration.GetSection("JwtSetting").Get<JwtSetting>();
//builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JwtSetting"));
builder.Services.Configure<JwtSetting>(_config.GetSection("JwtSetting"));

// middleware 
//builder.Services.AddTransient<JwtMidlleware>();

// Depedency Injection
builder.Services.UserModuleDI(_config);
builder.Services.GlobalUtilityDI();
builder.Services.ApiDI();

var app = builder.Build();

// Middleware
app.UseMiddleware<JwtMidlleware>();



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
