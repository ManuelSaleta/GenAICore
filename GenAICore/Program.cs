using GenAILib.Clients;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

Environment.SetEnvironmentVariable("OPENAI_API_KEY","");

app.MapGet("/health", () => "Healthy");

app.MapPost("/ask", async ([FromQuery(Name = "prompt")] string prompt) =>
{
    using var chatGptClient = new ChatGptClient();
    
    return await chatGptClient.ChatAsync(prompt);
});

app.Run();
