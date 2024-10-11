using Pantry.Data;
using Microsoft.AspNetCore.Mvc;
using Pantry.Models;
using System.Security.AccessControl;
using Pantry.Data;
using Microsoft.EntityFrameworkCore;
using static Pantry.Controllers.PantryController;
namespace Pantry.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase
    {

        private readonly PantryDBContext pantryDBContext;

        public RecipeController(PantryDBContext pantryDBContext)
        {
            this.pantryDBContext = pantryDBContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> Get()
        {
            return await pantryDBContext.Recipes.ToListAsync<Recipe>();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var RecipeById = await pantryDBContext.Recipes.FindAsync(Id);
            if (RecipeById == null)
            {
                return BadRequest("Item not found");
            }
            pantryDBContext.Recipes.Remove(RecipeById);
            await pantryDBContext.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult<Recipe>> GetByID(Guid Id)
        {
            var RecipeID = await pantryDBContext.Recipes.FindAsync(Id);
            if (RecipeID == null)
            {
                return BadRequest("Item not found");
            }
            return Ok(RecipeID);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, [FromBody] Recipe recipe)
        {
            var updateRecipe = await pantryDBContext.Recipes.FindAsync(Id);
            if (updateRecipe == null)
            {
                return BadRequest("Item not Found");
            }
            updateRecipe.Name = recipe.Name;
            updateRecipe.Ingredients = recipe.Ingredients;
            updateRecipe.ServingSize = recipe.ServingSize;
            updateRecipe.DifficultyLevel = recipe.DifficultyLevel;
            updateRecipe.CookingTime = recipe.CookingTime;
            updateRecipe.CookingInstructions = recipe.CookingInstructions;
            updateRecipe.DietaryRestrictions = recipe.DietaryRestrictions;
            updateRecipe.Quantities = recipe.Quantities;
            updateRecipe.Type = recipe.Type;

            await pantryDBContext.SaveChangesAsync();

            return NoContent();

        }

    }
}
