using Microsoft.EntityFrameworkCore;

namespace Pantry.Models
{
    [PrimaryKey("Guid")]
    public class Ingredient
    {
        public Guid Guid { get; private set; } = Guid.NewGuid();
        public string ItemName { get; set; } = "";
        public FoodType Type { get; set; }
    }

    public enum FoodType : int
    {
        Fruit,
        Vegetables,
        Grains,
        legumes,
        nuts_Seeds,
        Meat_Poultry,
        Fish_Seafood,
        Dairy_foods,
        Eggs
    }
}
