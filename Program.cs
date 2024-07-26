using Microsoft.OpenApi.Models;
using MinimalAPIMongoDB.Services;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependence Injection class MongoDbService
builder.Services.AddSingleton<MongoDbService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Minimal with MongoDb",
        Description = "Backend API",
        Contact = new OpenApiContact
        {
            Name = "Senai Informática"
        }
    });
});

// Build the app
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

// Use Services
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();

// Map Controller
app.MapControllers();

// Run Project
app.Run();
