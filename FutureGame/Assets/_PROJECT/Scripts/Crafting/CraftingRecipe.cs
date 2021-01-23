using UnityEngine;

public class CraftingRecipe
{

    private string _description;
    private int _finalItemId;
    private Vector2[] _itemsNeeded;

    public string Description { get { return _description; } }
    public int FinalItemId { get { return _finalItemId; } }
    public Vector2[] ItemsNeeded { get { return _itemsNeeded; } }

    public CraftingRecipe(int finalItemId, Vector2[] itemsNeeded, string description)
    {
        _finalItemId = finalItemId;
        _itemsNeeded = itemsNeeded;
        _description = description;
    }

}
