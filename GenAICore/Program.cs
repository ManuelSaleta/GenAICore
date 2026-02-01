using GenAICore.ConfigOptions;
using GenAICore.Controllers;
using GenAICore.Services;
using GenAILib.Clients;
using GenAILib.Parsers;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Automatic controller discovery + adds additional services for controllers.
builder.Services.AddControllers();

// If a service uses an external http client
builder.Services.AddHttpClient();
// Add your services
builder.Services.AddTransient<IAgentService, AgentService>();

// Add your env vars
builder.Services.AddOptions<AuthOptions>()
    .Bind(builder.Configuration.GetSection("Auth"))
    .Validate(opt => !string.IsNullOrWhiteSpace(opt.OpenAIApiKey), "OpenAIApiKey is required") 
    .ValidateOnStart();

// builder.Services.AddHttpClient<IChatGptClient, ChatGptClient>(client => new ChatGptClient())

var app = builder.Build();

// Map controller endpoints.
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.MapGet("/health", () => "Healthy");


app.Run();
