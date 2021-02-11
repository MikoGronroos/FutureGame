using UnityEngine;
using UnityEngine.EventSystems;

public class CraftSlot : MonoBehaviour, IPointerClickHandler
{

    private CraftingRecipe _slotRecipe;

    public CraftingRecipe SlotRecipe { get { return _slotRecipe; } set { _slotRecipe = value; } }
    public Vector2[] AmountOfUniqueIds;

    public void OnPointerClick(PointerEventData eventData)
    {
        Inventory.Instance.AddItemToInventory(ItemDictionary.Instance.GetItemByID(_slotRecipe.FinalItemId));
    }
}
