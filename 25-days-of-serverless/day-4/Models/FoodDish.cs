using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace day_4.Models
{
    public class FoodDish : IFoodDish
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string FromWho { get; set; }
    }
}
