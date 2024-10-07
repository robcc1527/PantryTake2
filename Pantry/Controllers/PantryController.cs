using Microsoft.AspNetCore.Mvc;
using Pantry.Models;
using System.Security.AccessControl;
using Pantry.Data;



namespace Pantry.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PantryController : ControllerBase
    {

        private static readonly Pantry _pantry = new Pantry(); // static creates only one instance is created and is used for all

        // create a full restful API - Delete/modify   

        private readonly PantryDBContext pantryDBContext;

        public PantryController(PantryDBContext pantryDBContext)
        {
            this.pantryDBContext = pantryDBContext;
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var pantryItemById = _pantry.GetById(Id);
            if(pantryItemById == null)
            {
                return BadRequest("Item not found");
            }
            _pantry.DeleteById(Id);
            return Ok();
        }

        [HttpPost("{Id}")]
        public IActionResult Update(int Id, [FromBody]PantryItem pantryItem)
        {
            var updatePantryItem = _pantry.myPantry.FirstOrDefault(i => i.PantryItemID == Id);
            if (updatePantryItem == null)
            {
                return BadRequest("Item not found");
            }
            updatePantryItem.ItemName = pantryItem.ItemName;
            updatePantryItem.Type = pantryItem.Type;
            updatePantryItem.Amount = pantryItem.Amount;
            
            return NoContent();            
        }


        [HttpPost(Name = "PostPantry")]
        public IActionResult Post([FromBody] PantryItem pantryItem)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int ProductID = _pantry.AddPantryItem(pantryItem);
            pantryItem.PantryItemID = ProductID;
            return Ok(pantryItem);
        }

       
        [HttpGet(Name = "GetPantry")]
        public PantryItem[] Get()
        {
            //return _pantry.GetAll();

            var results = pantryDBContext.PantryItems.ToArray();
            return results;
        }

        [HttpGet("{Id}")]
        public ActionResult<PantryItem> GetByID(int Id)
        {
            var pantryItemById = _pantry.GetById(Id);
            if(pantryItemById == null)
            {
                return BadRequest("Item not found");
            }
            return Ok(pantryItemById);
        }
    }
}
