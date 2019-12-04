using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;
using day_4.Models;

namespace day_4
{
    public static class GetFoodDishHttp
    {
        [FunctionName("GetFoodDishHttp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "food/{id}")] HttpRequest req,
            [Table(Constants.TableName, Constants.PartitionKey, "{id}")] FoodDishTableEntity foodDishTableEntity)
        {
            if (foodDishTableEntity == null)
                return new NotFoundResult();

            return new OkObjectResult(foodDishTableEntity);
        }
    }
}
