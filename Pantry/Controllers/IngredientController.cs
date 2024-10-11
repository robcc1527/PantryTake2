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
    public class IngredientController : ControllerBase
    {


        private readonly PantryDBContext pantryDBContext;

        public IngredientController(PantryDBContext pantryDBContext)
        {
            this.pantryDBContext = pantryDBContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> Get()
        {
            //return _pantry.GetAll()
            return await pantryDBContext.Ingredients.ToListAsync<Ingredient>();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Ingredient>> GetByID(Guid Id)
        {
            var ingredientID = await pantryDBContext.Ingredients.FindAsync(Id);
            if (ingredientID == null)
            {
                return BadRequest("Item not found");
            }
            return Ok(ingredientID);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, [FromBody]Ingredient ingredient)
        {
            var updateIngredient = await pantryDBContext.Ingredients.FindAsync(Id);
            if(updateIngredient == null)
            {
                return BadRequest("Item not Found");
            }
            updateIngredient.ItemName = ingredient.ItemName;
            updateIngredient.Weight = ingredient.Weight;
            updateIngredient.Sugar = ingredient.Sugar;
            updateIngredient.Fat = ingredient.Fat;
            updateIngredient.Carbohydrates = ingredient.Carbohydrates;
            updateIngredient.ImageUrl = ingredient.ImageUrl;
            updateIngredient.Protein = ingredient.Protein;
            updateIngredient.Type = ingredient.Type;

            await pantryDBContext.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var IngredientItemById = await pantryDBContext.Ingredients.FindAsync(Id);
            if (IngredientItemById == null)
            {
                return BadRequest("Item not found");
            }
            pantryDBContext.Ingredients.Remove(IngredientItemById);
            await pantryDBContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Ingredient newItem)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newIngrediant = new Ingredient()
            {
                
                ItemName = newItem.ItemName,
                Type = newItem.Type,
                Calories = newItem.Calories,
                Carbohydrates = newItem.Carbohydrates,
                Sugar = newItem.Sugar,
                Fat = newItem.Fat,
                Protein = newItem.Protein,
                Weight = newItem.Weight,
                ImageUrl = newItem.ImageUrl

            };
            var changedItem = await pantryDBContext.Ingredients.AddAsync(newIngrediant);
            var resultItem = changedItem.Entity;
            await pantryDBContext.SaveChangesAsync();
            return Ok(resultItem);
        }


    }
}

