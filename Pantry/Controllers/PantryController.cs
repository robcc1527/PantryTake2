using Microsoft.AspNetCore.Mvc;
using Pantry.Models;
using System.Security.AccessControl;
using Pantry.Data;
using Microsoft.EntityFrameworkCore;



namespace Pantry.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PantryController : ControllerBase
    {

        //private static readonly Pantry _pantry = new Pantry(); // static creates only one instance is created and is used for all

        // create a full restful API - Delete/modify   

        private readonly PantryDBContext pantryDBContext;

        public PantryController(PantryDBContext pantryDBContext)
        {
            this.pantryDBContext = pantryDBContext;
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int Id)
        {
            var pantryItemById = await pantryDBContext.PantryItems.FindAsync(Id);
            if(pantryItemById == null)
            {
                return BadRequest("Item not found");
            }
            pantryDBContext.PantryItems.Remove(pantryItemById);
            await pantryDBContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody]int amount)
        {
            var updatePantryItem = await pantryDBContext.PantryItems.FindAsync(Id);  //_pantry.myPantry.FirstOrDefault(i => i.PantryItemID == Id);
            if (updatePantryItem == null)
            {
                return BadRequest("Item not found");
            }
            updatePantryItem.Amount = amount;

            await pantryDBContext.SaveChangesAsync();
            
            return NoContent();            
        }


        [HttpPost(Name = "PostPantry")]
        public async Task<IActionResult> Post([FromBody] PantryItemCreateDTO newItem)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // check if the Ingredient acutally exists
            var ingredient = await pantryDBContext.Ingredients.FindAsync(newItem.IngredientID);
            if(ingredient == null)
            {
                return BadRequest("Ingredient not found");
            }

            var newPantryItem = new PantryItem()
            {
                Amount = newItem.Amount,
                Ingredient = ingredient
            };

            var changedItem = await pantryDBContext.PantryItems.AddAsync(newPantryItem); //_pantry.AddPantryItem(pantryItem);
            var resultItem = changedItem.Entity;

            await pantryDBContext.SaveChangesAsync();

            return Ok(resultItem);
        }

       
        [HttpGet(Name = "GetPantry")]
        public PantryItem[] Get()
        {
            //return _pantry.GetAll();

            var results = pantryDBContext.PantryItems.Include(p => p.Ingredient).ToArray();
            return results;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PantryItem>> GetByID(int Id)
        {
            var pantryItemById = await pantryDBContext.PantryItems.FindAsync(Id); //_pantry.GetById(Id);
            if(pantryItemById == null)
            {
                return BadRequest("Item not found");
            }
            return Ok(pantryItemById);
        }
    }

    public class PantryItemCreateDTO
    {
        public Guid IngredientID { get; set; }
        public int Amount { get; set; }
    }
}
