using Microsoft.EntityFrameworkCore;

namespace Pantry.Models
{
    [PrimaryKey("IngredientID")]    
        
    public class Ingredient
    {    
        public Guid IngredientID { get; private set; }
        public string ItemName { get; set; } = "";
        public FoodType Type { get; set; }
        public int Calories { get; set; }
        public double Carbohydrates { get; set; }
        public double Sugar { get; set; }
        public double Fat { get; set; }
        public double Protein { get; set; }
        public double Weight { get; set; }
        public string? ImageUrl { get; set; }

    }

    public enum FoodType : int
    {
        Fruit,              //0
        Vegetables,         //1
        Grains,             //2
        legumes,            //3
        nuts_Seeds,         //4    
        Meat_Poultry,       //5    
        Fish_Seafood,       //6    
        Dairy_foods,        //7    
        Eggs,               //8
        Drinks,             //9
        Alcohol,            //10
        CookingOils         //11
    }
}
