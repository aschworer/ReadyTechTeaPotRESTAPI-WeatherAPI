using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Numerics;

namespace ReadyTechTeaPotRESTAPI.WeatherService
{
    public class OpenWeatherAPIService : IWeatherService
    {
        //should be outside of code - best to keep it in the environment variables. Please do not misuse it ;)
        public static string WEATHER_API_KEY = "ea93fb19d4dfb4852c369ded7bd6d9f7";

        public async Task<float> GetTemperature()
        {
            var url = $"http://api.openweathermap.org/data/2.5/forecast";
            //Weather for Brisbane in metric units
            var parameters = $"?id=524901&appid={WEATHER_API_KEY}&lat=-27.5&lon=153&units=metric";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(parameters).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var weatherData = JsonConvert.DeserializeObject<WeatherData>(data);
                return weatherData.list[0].main.temp;
            }
            else
            {
                return -999999; //need more elegant response here, but I'll leave it out of scope
            }

        }
    }
}
