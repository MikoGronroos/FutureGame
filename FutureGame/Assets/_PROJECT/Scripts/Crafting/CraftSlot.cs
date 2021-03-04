using UnityEngine;
using UnityEngine.EventSystems;

public class CraftSlot : MonoBehaviour, IPointerClickHandler
{

    private CraftingRecipe _slotRecipe;

    public CraftingRecipe SlotRecipe { get { return _slotRecipe; } set { _slotRecipe = value; } }
    public Vector2[] AmountOfUniqueIds;

    public void OnPointerClick(PointerEventData eventData)
    {

        CraftItem();

    }

    private void CraftItem()
    {
        for (int i = 0; i < _slotRecipe.ItemsNeeded.Length; i++)
        {
            if (!Inventory.Instance.CheckIfInventoryHasAmountOfItems((int)_slotRecipe.ItemsNeeded[i].x, (int)_slotRecipe.ItemsNeeded[i].y))
            {
                return;
            }
        }

        for (int i = 0; i < _slotRecipe.ItemsNeeded.Length; i++)
        {
            Inventory.Instance.RemoveMultipleItemsFromInventoryWithId((int)_slotRecipe.ItemsNeeded[i].x, (int)_slotRecipe.ItemsNeeded[i].y);
        }
        Inventory.Instance.AddToInventory(ItemDictionary.Instance.GetItemByID(_slotRecipe.FinalItemId), 1);
    }

}
