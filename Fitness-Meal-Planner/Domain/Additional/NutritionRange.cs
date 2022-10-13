using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Additional_Structures
{
    //ranges used to limit product/meals that will be shown 
    public class NutritionRange
    {
        public uint MinCalories { get; set; }
        public uint MaxCalories { get; set; } 
        public uint MinProtein { get; set; }
        public uint MaxProtein { get; set; } 
        public uint MinCarbohydrates { get; set; }
        public uint MaxCarbohydrates { get; set; }
        public uint MinFat { get; set; }
        public uint MaxFat { get; set; } 
        public string Name { get; set; }
        public NutritionRange(uint minCalories, uint maxCalories, uint minProtein, uint maxProtein,
            uint minCarbohydrates, uint maxCarbohydrates, uint minFat, uint maxFat, string name)
        {
            MinCalories = minCalories;
            MaxCalories = maxCalories;
            MinProtein = minProtein;
            MaxProtein = maxProtein;
            MinCarbohydrates = minCarbohydrates;
            MaxCarbohydrates = maxCarbohydrates;
            MinFat = minFat;
            MaxFat = maxFat;
            Name = name;
        }
    }
}
