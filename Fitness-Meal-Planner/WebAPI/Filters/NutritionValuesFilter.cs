namespace WebAPI.Filters
{
    // filter used to limit products/meals that will be shown, based on nutrition values
    public class NutritionValuesFilter
    {
        public uint MinCalories { get; set; }
        public uint MaxCalories { get; set; } = 9999;
        public uint MinProtein { get; set; }
        public uint MaxProtein { get; set; } = 999;
        public uint MinCarbohydrates { get; set; }
        public uint MaxCarbohydrates { get; set; } = 999;
        public uint MinFat { get; set; }
        public uint MaxFat { get; set; } = 999;
        public string Name { get; set; } = "";

        public NutritionValuesFilter(uint minCalories, uint maxCalories, uint minProtein, uint maxProtein,
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

        public NutritionValuesFilter() { }

        public bool ValidFilterValues()
        {
            return (MaxCalories >= MinCalories && MaxProtein >= MinProtein &&
            MaxCarbohydrates >= MinCarbohydrates && MaxFat >= MinFat);
        } 
    }
}
