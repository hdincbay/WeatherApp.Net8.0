using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using WeatherApp.WebUI.Models;

namespace WeatherApp.WebUI.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(string lat, string lon, string apiKey)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}");
            if(responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<WeatherData>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
