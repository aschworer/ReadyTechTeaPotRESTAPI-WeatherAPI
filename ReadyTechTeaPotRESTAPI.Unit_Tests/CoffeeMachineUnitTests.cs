using Microsoft.Extensions.Logging;
using ReadyTechTeaPotRESTAPI.Controllers;
using ReadyTechTeaPotRESTAPI.Models;
using ReadyTechTeaPotRESTAPI.Util;
using Moq;
using Microsoft.AspNetCore.Http;
using ReadyTechTeaPotRESTAPI.WeatherService;

namespace CoffeeMachineTests
{
    [TestClass]
    public class CoffeeMachineControllerTests
    {
        [TestMethod]
        public void BrewCoffee()
        {
            var mock = new Mock<ILogger<CoffeeMachineController>>();
            ILogger<CoffeeMachineController> logger = mock.Object;
            var coffeeMachineController = new CoffeeMachineController(logger);
            coffeeMachineController.ControllerContext.HttpContext = new DefaultHttpContext();
            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(arg => arg.GetTemperature()).ReturnsAsync(20.0f);
            coffeeMachineController.weatherService = weatherServiceMock.Object;
            for (int i = 0; i < 4; i++)
            {
                var response = coffeeMachineController.Post();
                Assert.IsNotNull(response);
                Assert.IsTrue(response.GetType() == typeof(SuccessResponse));
                Assert.AreEqual("Your piping hot coffee is ready", ((SuccessResponse)response).Message);
            }

            //check that it's 503 after 4th coffee
            var outOfCoffeeResponse = coffeeMachineController.Post();
            Assert.IsNotNull(outOfCoffeeResponse);
            Assert.IsTrue(outOfCoffeeResponse.GetType() == typeof(OutOfCoffeeResponse));

            //check that after 503 it's again 200
            var successResponse = coffeeMachineController.Post();
            Assert.IsNotNull(successResponse);
            Assert.IsTrue(successResponse.GetType() == typeof(SuccessResponse));
            Assert.AreEqual("Your piping hot coffee is ready", ((SuccessResponse)successResponse).Message);

        }
        
        [TestMethod]
        public void FirstOfAprilBrew()
        {          var mock = new Mock<ILogger<CoffeeMachineController>>();
                   ILogger<CoffeeMachineController> logger = mock.Object;
                   var coffeeMachineController = new CoffeeMachineController(logger);
                   coffeeMachineController.ControllerContext.HttpContext = new DefaultHttpContext();
            var timeMock = new Mock<IDateProvider>();
            timeMock.Setup(m => m.Now()).Returns(new DateTime(2010, 4, 1).ToLocalTime());
            coffeeMachineController.dateProvider = timeMock.Object;
            var response = coffeeMachineController.Post();
            Assert.IsNotNull(response);
            Assert.IsTrue(response.GetType() == typeof(SuccessResponse));
        }

        [TestMethod]
        public void IcedCoffeeBrew()
        {
            var mock = new Mock<ILogger<CoffeeMachineController>>();
            ILogger<CoffeeMachineController> logger = mock.Object;
            var coffeeMachineController = new CoffeeMachineController(logger);
            coffeeMachineController.ControllerContext.HttpContext = new DefaultHttpContext();
            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(arg => arg.GetTemperature()).ReturnsAsync(35.0f);
            coffeeMachineController.weatherService = weatherServiceMock.Object;
            var response = coffeeMachineController.Post();
            Assert.IsNotNull(response);
            Assert.IsTrue(response.GetType() == typeof(SuccessResponse));
            Assert.AreEqual("Your refreshing iced coffee is ready", ((SuccessResponse)response).Message);
        }

    }
}