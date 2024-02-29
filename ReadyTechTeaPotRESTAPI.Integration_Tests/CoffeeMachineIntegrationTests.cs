using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace IntegratonTests
{
    public class IntegratonTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public IntegratonTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/brew-coffee")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();


            //loop 4 times
            for (int i = 0; i < 4; i++)
            {
                // Act
                var response = await client.PostAsync(url, null);

                // Assert
                response.EnsureSuccessStatusCode(); // Status Code 200-299
                Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            }

            //5th time - 503

            // Act
            var response503 = await client.PostAsync(url, null);

            // Assert
            //print status code
            Console.WriteLine(response503.StatusCode);
            Assert.Equal(HttpStatusCode.ServiceUnavailable, response503.StatusCode); // Status Code 200-299

        }

        
        public async Task Get_EndpointsReturn418AndCorrectContentType(string url)
        {
            
            //mocking the date here requires some extra effort, so leaving this out of scope for now, but definetely needs to be tested as well


        }

        public async Task Get_EndpointsReturnsIcedCoffee(string url)
        {

            //mocking the weather here some extra effort, so leaving this out of scope for now, but definetely needs to be tested as well


        }

    }
}