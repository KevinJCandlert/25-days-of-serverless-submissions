using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using day_4.Models;
using System.Collections.Generic;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Http.Extensions;

namespace day_4
{
    public static class AddFoodDishHttp
    {
        [FunctionName("AddFoodDishHttp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "food")] HttpRequest req,
            [Table(Constants.TableName)] IAsyncCollector<FoodDishTableEntity> foodDishesTable,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (!requestBody.IsValidJson<FoodDish>(out IList<string> errorMessages))
                return new BadRequestObjectResult(errorMessages);

            FoodDish incommingFoodDish = JsonConvert.DeserializeObject<FoodDish>(requestBody);
            var foodDishTableEntity = FoodDishTableEntity.Convert(incommingFoodDish);
            await foodDishesTable.AddAsync(foodDishTableEntity);

            log.LogInformation($"FoodDish:{foodDishTableEntity.RowKey} has been added.");

            var baseUri = new Uri(req.GetEncodedUrl());
            var newDishUri = new Uri(baseUri, $"food/{foodDishTableEntity.RowKey}");
            return new CreatedResult(newDishUri.ToString(), new { location = newDishUri });
        }
    }
}
