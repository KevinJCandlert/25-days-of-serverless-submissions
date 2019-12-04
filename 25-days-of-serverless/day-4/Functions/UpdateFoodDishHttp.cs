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
using System.Collections.Generic;

namespace day_4.Functions
{
    public static class UpdateFoodDishHttp
    {
        [FunctionName("UpdateFoodDishHttp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "food/{id}")] HttpRequest req,
            [Table(Constants.TableName)] CloudTable foodDishTable,
            [Table(Constants.TableName, Constants.PartitionKey, "{id}")] FoodDishTableEntity foodDishTableEntity,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (foodDishTableEntity == null)
                return new NotFoundResult();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (!requestBody.IsValidJson<FoodDish>(out IList<string> errorMessages))
                return new BadRequestObjectResult(errorMessages);

            FoodDish incommingFoodDish = JsonConvert.DeserializeObject<FoodDish>(requestBody);
            foodDishTableEntity.Replace(incommingFoodDish);

            var replaceOperation = TableOperation.Replace(foodDishTableEntity);
            await foodDishTable.ExecuteAsync(replaceOperation);

            log.LogInformation($"FoodDish:{foodDishTableEntity.RowKey} has been updated.");

            return new OkResult();
        }
    }
}
