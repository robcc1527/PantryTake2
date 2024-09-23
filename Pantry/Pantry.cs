namespace Pantry
{
    public class Pantry
    {
        public List<PantryItem> myPantry = new List<PantryItem>()
        {
            new PantryItem()  { PantryItemID = 1, ItemName = "Carrot", Type = FoodType.Vegetables, Amount = 5 },
            new PantryItem()  { PantryItemID = 2, ItemName = "Apple", Type = FoodType.Fruit, Amount = 8 }

        };         

            public int AddPantryItem(PantryItem pantryItem)
        {
            pantryItem.PantryItemID = 3;

            myPantry.Add(pantryItem);

            return pantryItem.PantryItemID;
        }

        public PantryItem GetById(int Id)
        {
            var pantryItem = myPantry.FirstOrDefault(u => u.PantryItemID == Id);
            return pantryItem;
        }

        public PantryItem[] GetAll()
        {
            return myPantry.ToArray();
        }

        public void DeleteById(int Id)
        {
            var pantryItem = myPantry.FirstOrDefault(u => u.PantryItemID == Id);
            if (pantryItem != null)
            {
                myPantry.Remove(pantryItem);
            }
        }

        
    }
}
