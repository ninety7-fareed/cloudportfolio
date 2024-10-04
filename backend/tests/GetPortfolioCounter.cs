using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Company.Function
{
    public static class GetPortfolioCounter
    {
        public static async Task<IActionResult> Run(
            HttpRequest req,
            Counter counter,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            counter.Count++;

            return new OkObjectResult(counter);
        }
    }
}