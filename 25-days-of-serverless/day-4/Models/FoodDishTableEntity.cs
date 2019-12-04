using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace day_4.Models
{
    public class FoodDishTableEntity : TableEntity, IFoodDish
    {
        public static FoodDishTableEntity Convert(IFoodDish foodDish)
        {
            var guid = Guid.NewGuid().ToString();
            return new FoodDishTableEntity()
            {
                Description = foodDish.Description,
                FromWho = foodDish.FromWho,
                Name = foodDish.Name,
                PartitionKey = Constants.PartitionKey,
                RowKey = guid
            };
        }

        public FoodDishTableEntity Replace(IFoodDish incommingFoodDish)
        {
            Name = incommingFoodDish.Name;
            Description = incommingFoodDish.Description;
            FromWho = incommingFoodDish.FromWho;

            return this;
        }

        public string Description { get; set; }
        public string FromWho { get; set; }
        public string Name { get; set; }
    }
}
