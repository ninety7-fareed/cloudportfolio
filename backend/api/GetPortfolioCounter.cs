using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class GetPortfolioCounter
    {
        public class Counter
        {
            public string id { get; set; }
            public int Count { get; set; }
        }

        [FunctionName("GetPortfolioCounter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "AzurePortfolio",
                containerName: "Counter",
                Connection = "AzurePortfolioConnectionString",
                Id = "1",
                PartitionKey = "1")] Counter counter,
            [CosmosDB(
                databaseName: "AzurePortfolio",
                containerName: "Counter",
                Connection = "AzurePortfolioConnectionString")] IAsyncCollector<Counter> counterOut,
            ILogger log)
        {
            //This right here is where the counter gets updated.
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (counter == null)
            {
                log.LogWarning("Counter not found in database, initializing new counter.");
                counter = new Counter { id = "1", Count = 0 };
            }

            var updatedCounter = new Counter
            {
                id = counter.id,
                Count = counter.Count + 1
            };

            await counterOut.AddAsync(updatedCounter);

            var response = new { Count = updatedCounter.Count };
            log.LogInformation($"Returning response: {JsonConvert.SerializeObject(response)}");

            return new OkObjectResult(response);
        }
    }
}