namespace WebAPI.Filters
{
    public class NutritionValuesFilter
    {
        public uint minCalories { get; set; }
        public uint maxCalories { get; set; } = 9999;
        public uint minProtein { get; set; }
        public uint maxProtein { get; set; } = 999;
        public uint minCarbohydrates { get; set; }
        public uint maxCarbohydrates { get; set; } = 999;
        public uint minFat { get; set; }
        public uint maxFat { get; set; } = 999;
        public string name { get; set; } = "";
        public NutritionValuesFilter(uint _minCalories, uint _maxCalories, uint _minProtein, uint _maxProtein,
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
        public NutritionValuesFilter() { }

        public bool ValidFilterValues()
        {
            return (maxCalories >= minCalories && maxProtein >= minProtein &&
            maxCarbohydrates >= minCarbohydrates && maxFat >= minFat);
        } 
    }
}
