using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Company.Function;
using tests;

namespace tests
{
    public class TestCounter
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async Task GetPortfolioCounter_ShouldIncrementCount()
        {
            // Arrange
            var counter = new Counter
            {
                Id = "1",
                Count = 2
            };
            var request = TestFactory.CreateHttpRequest();

            // Act
            var response = await GetPortfolioCounter.Run(request, counter, logger);

            // Assert
            Assert.IsType<OkObjectResult>(response);
            var result = (OkObjectResult)response;
            Assert.IsType<Counter>(result.Value);
            var returnedCounter = (Counter)result.Value;
            Assert.Equal("1", returnedCounter.Id);
            Assert.Equal(3, returnedCounter.Count);
        }
    }
}