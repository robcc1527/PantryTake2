using Microsoft.EntityFrameworkCore;

namespace Pantry.Models
{
    [PrimaryKey("PantryItemID")]
    public class PantryItem
    {
        public Guid PantryItemID { get; private set; } = Guid.NewGuid();
        public Ingredient Ingredient { get; set; }
        public int Amount { get; set; }

    }


}
