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
    public static class ListFoodDishesHttp
    {
        [FunctionName("ListFoodDishesHttp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "food")] HttpRequest req,
            [Table(Constants.TableName)] CloudTable foodDishTable)
        {
            var query = new TableQuery<FoodDishTableEntity>();
            var entities = await foodDishTable.ExecuteQuerySegmentedAsync(query, null); //This will only fetch the first 1000 items.

            return new OkObjectResult(entities);
        }
    }
}
