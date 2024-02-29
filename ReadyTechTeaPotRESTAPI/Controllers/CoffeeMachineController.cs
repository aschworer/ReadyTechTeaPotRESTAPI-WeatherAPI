using Microsoft.AspNetCore.Mvc;
using ReadyTechTeaPotRESTAPI.Models;
using ReadyTechTeaPotRESTAPI.Util;
using ReadyTechTeaPotRESTAPI.WeatherService;

namespace ReadyTechTeaPotRESTAPI.Controllers
{
    [ApiController]
    [Route("/")]
    public class CoffeeMachineController : Controller
    {
        /**
         * The variable here is internal to the controller, so whenever the server is restarted, the count will be reset to 0.
         * In order to not reset the count and make it fault-tolerant, the count should be stored in a database or a file.
         * Since the time is limited for this assinment, I will not involve the database.
         * */
        private static int s_brewCount = 0;

        public IDateProvider dateProvider { get; set; }
        public IWeatherService weatherService { get; set; }


        private readonly ILogger<CoffeeMachineController> _logger;

        public CoffeeMachineController(ILogger<CoffeeMachineController> logger)
        {
            dateProvider = new SystemTime();
            weatherService = new OpenWeatherAPIService();
            _logger = logger;
        }

        [HttpPost]
        [Route("brew-coffee")]
        public ICoffeeBrewerResponse Post()
        {
            if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
            {
                this.HttpContext.Response.StatusCode = 418; // I'm a teapot
                return new IAmTeapotResponse();
            }

            var successMessage = "Your piping hot coffee is ready";

            var temperature = weatherService.GetTemperature().GetAwaiter().GetResult();
            if (temperature > 30) successMessage = "Your refreshing iced coffee is ready";

            if (s_brewCount == 4)
            {
                s_brewCount = 0;
                this.HttpContext.Response.StatusCode = 503; // out of coffee
                return new OutOfCoffeeResponse();
            } else
            {
                s_brewCount++;
                string[] splitTimestamp = dateProvider.Now().ToString("o").Split('.');
                string timestamp = splitTimestamp[0] + "+" + splitTimestamp[1].Split('+')[1];
                return new SuccessResponse(successMessage, timestamp);
            }
        }

    }
}
