using Example.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IWeatherService, WeatherService>();
builder.Services.AddOutputCache();
builder.Services.AddStackExchangeRedisOutputCache(options =>
{
    options.InstanceName = "WeatherApi";
    options.Configuration = "localhost:6379";
});

var app = builder.Build();
app.UseOutputCache();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/getWeatherByCity/{city}", (string city, IWeatherService service) => 
        service.GetWeatherForecastByCityAsync(city))
    .CacheOutput(x => x.Expire(TimeSpan.FromMinutes(5)))
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();