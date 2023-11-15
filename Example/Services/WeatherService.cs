using System.Text.Json;
using redisExample.Models;

namespace Example.Services;

public interface IWeatherService
{
   Task<Root?> GetWeatherForecastByCityAsync(string city); 
}

public class WeatherService : IWeatherService
{
   private readonly HttpClient _client;
   private readonly IConfiguration _configuration;
   
   public WeatherService(HttpClient client, IConfiguration configuration)
   {
      _client = client;
      _configuration = configuration;
   }

   public async Task<Root?> GetWeatherForecastByCityAsync(string city)
   {
      var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_configuration["ApiKey"]}";
      var response = await _client.GetAsync(url);
      response.EnsureSuccessStatusCode();
      var stringResponse = await response.Content.ReadAsStringAsync();
      return JsonSerializer.Deserialize<Root>(stringResponse);
   }
}