using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftSlot : MonoBehaviour, IPointerClickHandler
{

    private CraftingRecipe _slotRecipe;

    public CraftingRecipe SlotRecipe { get { return _slotRecipe; } set { _slotRecipe = value; } }
    public Vector2[] AmountOfUniqueIds;

    public void OnPointerClick(PointerEventData eventData)
    {

        if (InventoryMethods.CheckIfInventoryHasItems(AmountOfUniqueIds))
        {
            InventoryMethods.RemoveItemsFromInventory(AmountOfUniqueIds);
            CharacterOwner.Instance.Inventory.AddItem(_slotRecipe.FinalItemId);
        }
        else
        {
            return;
        }
    }
}
