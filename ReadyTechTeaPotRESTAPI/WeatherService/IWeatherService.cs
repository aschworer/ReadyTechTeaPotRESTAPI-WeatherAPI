namespace ReadyTechTeaPotRESTAPI.WeatherService
{
    public interface IWeatherService
    {
        public Task<float> GetTemperature();
    }
}
