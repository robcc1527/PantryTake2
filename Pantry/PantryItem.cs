namespace Pantry
{
    public class PantryItem
    {
        public int PantryItemID { get; set; }
        public string ItemName { get; set; } = "";
        public FoodType Type { get; set; }
        public int Amount { get; set; }

    }

    public enum FoodType : int
    {
        Fruit,
        Vegetables,
        Grains, 
        legumes, 
        nuts_Seeds,
        Meat_Poultry,
        Fish_Seafood,
        Dairy_foods,
        Eggs
    }
}
