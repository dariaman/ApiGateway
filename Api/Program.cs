using Api;
using Api.RequestValidator;
using GlobalUtility;
using UserModule;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Depedency Injection
builder.Services.RegisterDIEntity();
builder.Services.RegisterDIGlobalUtility();
builder.Services.RegisterApiInjection();

// 
builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(FormatValidationAttribute));
    options.AllowEmptyInputInBodyModelBinding = true;
});

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
