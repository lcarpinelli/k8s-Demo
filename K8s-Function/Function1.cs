using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace K8s_Function
{
    public class Function1
    {
        private readonly IConfiguration configuration;

        public Function1(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            HttpClient client = new();
            string responseMessage;
            //var baseUrl = "http://backend/WeatherForecast";
            var baseUrlBackend = configuration.GetSection("Api:BaseUrlBackend").Value +("/WeatherForecast");
            try
            {
                log.LogInformation("Inizio chiamata");
                var result = await client.GetAsync(baseUrlBackend);
                var responseString = await result.Content.ReadAsStringAsync();
                responseMessage = responseString;
            }
            catch (Exception ex)
            {
                responseMessage = "Function error";
            }

            return new OkObjectResult(responseMessage);
        }
    }
}
