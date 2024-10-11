
using Microsoft.EntityFrameworkCore;

namespace Pantry.Models
{
    [PrimaryKey("RecipeID")]
    public class Recipe
    {
        public Guid RecipeID { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public double Quantities { get; set; }
        public string CookingInstructions { get; set; }
        public int CookingTime { get; set; }
        public double DifficultyLevel { get; set; }
        public CuisineType Type { get; set; }
        public string DietaryRestrictions { get; set; }
        public double ServingSize { get; set; }
    }

    public enum CuisineType : int
    {
        Italian,         //0
        Chinese,         //1
        Indian,          //2
        Mexican,         //3
        French,          //4    
        Japanese,        //5    
        Mediterranean,   //6    
        Thai,            //7    
        Greek,           //8
        American,        //9
    }
}
