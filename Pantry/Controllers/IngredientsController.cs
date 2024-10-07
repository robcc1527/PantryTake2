using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pantry.Data;
using Pantry.Models;

namespace Pantry.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly PantryDBContext pantryDBContext;

        // create a full restful API - Delete/modify   

        public IngredientsController(PantryDBContext pantryDBContext)
        {
            this.pantryDBContext = pantryDBContext;
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int Id)
        {
            var ingredientByID = await pantryDBContext.Ingredients.FindAsync(Id);
            if(ingredientByID == null)
            {
                return BadRequest("Item not found");
            }
            pantryDBContext.Ingredients.Remove(ingredientByID);
            await pantryDBContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody]Ingredient passedIngredient)
        {
            var updatePantryItem = await pantryDBContext.PantryItems.FindAsync(Id);  //_pantry.myPantry.FirstOrDefault(i => i.PantryItemID == Id);
            if (updatePantryItem == null)
            {
                return BadRequest("Item not found");
            }
            updatePantryItem.Ingredient.ItemName = passedIngredient.ItemName;
            updatePantryItem.Ingredient.Type = passedIngredient.Type;

            await pantryDBContext.SaveChangesAsync();
            
            return NoContent();            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> Get()
        {
            return await pantryDBContext.Ingredients.ToListAsync<Ingredient>();
        }

        [HttpPost(Name = "PostIngredient")]
        public async Task<IActionResult> Post([FromBody] Ingredient ingredient)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            await pantryDBContext.Ingredients.AddAsync(ingredient);
            await pantryDBContext.SaveChangesAsync();
            return Ok(ingredient);
        }
    }
}
