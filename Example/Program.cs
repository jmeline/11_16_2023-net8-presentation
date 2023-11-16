using Example.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IWeatherService, WeatherService>();

int[] values = [9,9,9,9,9];
Span<int> numbers = [1,2,3,4,5];
List<int> spans = [..numbers, 6,7,8,9,10, ..values];
Console.WriteLine(string.Join(", ", spans));

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