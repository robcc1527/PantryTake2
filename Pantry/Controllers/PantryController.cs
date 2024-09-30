using Microsoft.AspNetCore.Mvc;
using Pantry.Data;
using Pantry.Models;
using System.Security.AccessControl;


namespace Pantry.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PantryController : ControllerBase
    {

        private static readonly Pantry _pantry = new Pantry(); // static creates only one instance is created and is used for all
        
        private readonly PantryDBContext pantryDBContext;

        // create a full restful API - Delete/modify   

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
            var result = pantryDBContext.PantryItems.ToArray();
            return result;
        }

        [HttpGet("{Id}")]
        public ActionResult<PantryItem> GetByID(int Id)
        {
            //var pantryItemByID = pantryDBContext.PantryItems.Find(Id);

            var pantryItemByID = pantryDBContext.PantryItems.Where(item => item.PantryItemID == Id).FirstOrDefault();

            if (pantryItemByID == null)
            {
                return NotFound();
            }

            return pantryItemByID;
        }
    }
}
