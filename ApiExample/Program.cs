using System.Text.Json.Serialization;
using ApiExample.Infrastructure;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddProblemDetails();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(configure =>
{
    configure.Title = "User settings API";
    configure.Version = "1.0";
    configure.Description = "A simple API to manage user settings";
    configure.DocumentName = "v1";
    
    configure.OperationProcessors.Add(new ProblemDetailsResponseContentTypeProcessor());

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();