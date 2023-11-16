using Example;
using Example.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IWeatherService, WeatherService>();

builder.Services.AddKeyedSingleton<INotificationService, HappyNotificationService>(nameof(HappyNotificationService));
builder.Services.AddKeyedSingleton<INotificationService, AngryNotificationService>(nameof(AngryNotificationService));
builder.Services.AddKeyedSingleton<INotificationService, SillyNotificationService>(nameof(SillyNotificationService));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/getWeatherByCity/{city}", (string city, IWeatherService service) => 
        service.GetWeatherForecastByCityAsync(city))
    .WithName("GetWeather")
    .WithOpenApi();

app.Run();
