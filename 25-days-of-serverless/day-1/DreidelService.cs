using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;

namespace day_1
{
    public static class DreidelService
    {
        /// <summary>
        /// randomly returns נ (Nun), ג (Gimmel), ה (Hay), or ש (Shin)
        /// </summary>
        [FunctionName("DreidelService")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            var dreidelValues = new[] { "נ", "ג", "ה", "ש" };
            var retval = dreidelValues.OrderBy(dV => Guid.NewGuid()).First();
            log.LogInformation($"Spinning dreidel... Returning {retval}");
            return new OkObjectResult(retval);
        }
    }
}
