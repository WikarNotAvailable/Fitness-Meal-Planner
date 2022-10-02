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
        public uint minCalories { get; set; }
        public uint maxCalories { get; set; } 
        public uint minProtein { get; set; }
        public uint maxProtein { get; set; } 
        public uint minCarbohydrates { get; set; }
        public uint maxCarbohydrates { get; set; }
        public uint minFat { get; set; }
        public uint maxFat { get; set; } 
        public string name { get; set; }
        public NutritionRange(uint _minCalories, uint _maxCalories, uint _minProtein, uint _maxProtein,
            uint _minCarbohydrates, uint _maxCarbohydrates, uint _minFat, uint _maxFat, string _name)
        {
            minCalories = _minCalories;
            maxCalories = _maxCalories;
            minProtein = _minProtein;
            maxProtein = _maxProtein;
            minCarbohydrates = _minCarbohydrates;
            maxCarbohydrates = _maxCarbohydrates;
            minFat = _minFat;
            maxFat = _maxFat;
            name = _name;
        }
    }
}
