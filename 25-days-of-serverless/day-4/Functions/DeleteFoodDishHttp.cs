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
    public static class DeleteFoodDishHttp
    {
        [FunctionName("DeleteFoodDishHttp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "food/{id}")] HttpRequest req,
            [Table(Constants.TableName)] CloudTable foodDishTable,
            [Table(Constants.TableName, Constants.PartitionKey, "{id}")] FoodDishTableEntity foodDishTableEntity,
            ILogger log)
        {
            if (foodDishTableEntity == null)
                return new NotFoundResult();

            var deleteOperation = TableOperation.Delete(foodDishTableEntity);
            await foodDishTable.ExecuteAsync(deleteOperation);

            log.LogInformation($"FoodDish:{foodDishTableEntity.RowKey} has been deleted.");
            return new NoContentResult();
        }
    }
}
