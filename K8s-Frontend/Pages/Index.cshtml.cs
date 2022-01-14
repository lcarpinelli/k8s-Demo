using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace K8s_Frontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _conf;
        public string WeatherCondition { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration conf)
        {
            _logger = logger;
            _conf = conf;
        }

        public async Task OnGet()
        {

        }

        public async Task OnPost(int num)
        {
            HttpClient client = new();
            var baseUrl = _conf.GetSection("Api:BaseUrlBackend").Value + "/WeatherForecast";
            if (num == 1)
            {
                baseUrl = _conf.GetSection("Api:BaseUrl").Value + "/api/function1";
            }
            try
            {
                var result = await client.GetAsync(baseUrl);
                var responseString = await result.Content.ReadAsStringAsync();
                this.WeatherCondition = responseString;
            }
            catch (Exception ex)
            {
                this.WeatherCondition = ex.Message;
            }
        }
    }
}
