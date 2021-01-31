using UnityEngine;

public class CraftingRecipe
{

    private string _description;
    private int _finalItemId;
    private Vector2[] _itemsNeeded;
    private bool _isDefaultRecipe;

    public string Description { get { return _description; } }
    public int FinalItemId { get { return _finalItemId; } }
    public Vector2[] ItemsNeeded { get { return _itemsNeeded; } }
    public bool IsDefaultRecipe { get { return _isDefaultRecipe; } }

    public CraftingRecipe(int finalItemId, Vector2[] itemsNeeded, string description, bool isDefault = false)
    {
        _finalItemId = finalItemId;
        _itemsNeeded = itemsNeeded;
        _description = description;
        _isDefaultRecipe = isDefault;
    }

}
