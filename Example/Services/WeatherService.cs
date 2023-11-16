using System.Text.Json;
using redisExample.Models;

namespace Example.Services;

public interface IWeatherService
{
   Task<Root?> GetWeatherForecastByCityAsync(string city); 
}

public class WeatherService(HttpClient client, IConfiguration configuration) : IWeatherService
{
   public async Task<Root?> GetWeatherForecastByCityAsync(string city)
   {
      var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={configuration["ApiKey"]}";
      var response = await client.GetAsync(url);
      response.EnsureSuccessStatusCode();
      var stringResponse = await response.Content.ReadAsStringAsync();
      return JsonSerializer.Deserialize<Root>(stringResponse);
   }
}