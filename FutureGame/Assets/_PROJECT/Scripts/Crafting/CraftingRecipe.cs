public class CraftingRecipe
{

    private string _description;
    private int _finalItemId;
    private int[] _itemsNeeded;

    public string Description { get { return _description; } }
    public int FinalItemId { get { return _finalItemId; } }
    public int[] ItemsNeeded { get { return _itemsNeeded; } }

    public CraftingRecipe(int finalItemId, int[] itemsNeeded, string description)
    {
        _finalItemId = finalItemId;
        _itemsNeeded = itemsNeeded;
        _description = description;
    }

}
