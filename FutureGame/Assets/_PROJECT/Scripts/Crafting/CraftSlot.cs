using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftSlot : MonoBehaviour, IPointerClickHandler
{

    private CraftingRecipe _slotRecipe;

    public CraftingRecipe SlotRecipe { get { return _slotRecipe; } set { _slotRecipe = value; } }

    public void OnPointerClick(PointerEventData eventData)
    {

        Vector2[] amountOfUniqueIds = GetAmountOfUniqueIds(_slotRecipe.ItemsNeeded);

        for (int i = 0; i < amountOfUniqueIds.Length; i++)
        {
            if (CharacterOwner.Instance.Inventory.GetAmountOfItemsInDictionary((int)amountOfUniqueIds[i].x) < (int)amountOfUniqueIds[i].y)
            {
                return;
            }
        }

        for (int x = 0; x < amountOfUniqueIds.Length; x++)
        {
            for (int y = 0; y < amountOfUniqueIds[x].y; y++)
            {
                CharacterOwner.Instance.Inventory.RemoveItemFromInventoryWithId((int)amountOfUniqueIds[x].x);
            }
        }

        CharacterOwner.Instance.Inventory.AddItem(_slotRecipe.FinalItemId);

    }

    public Vector2[] GetAmountOfUniqueIds(int[] arrayOfIds)
    {
        List<int> uniqueIds = new List<int>();
        for (int i = 0; i < arrayOfIds.Length; i++)
        {
            if (!uniqueIds.Contains(arrayOfIds[i]))
            {
                uniqueIds.Add(arrayOfIds[i]);
            }
        }

        List<Vector2> returningList = new List<Vector2>();

        for (int i = 0; i < uniqueIds.Count; i++)
        {
            returningList.Add(new Vector2(uniqueIds[i], 0));
        }

        for (int j = 0; j < arrayOfIds.Length; j++)
        {
            for (int k = 0; k < returningList.Count; k++)
            {
                if (arrayOfIds[j] == (int)returningList[k].x)
                {
                    int amount = (int)returningList[k].y + 1;
                    Vector2 item = new Vector2(returningList[k].x, amount);
                    returningList[k] = item;
                }
            }
        }
        return returningList.ToArray();
    }
}
