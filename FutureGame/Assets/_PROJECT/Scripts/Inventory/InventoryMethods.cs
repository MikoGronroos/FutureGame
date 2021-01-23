using UnityEngine;

public class InventoryMethods
{

    public static bool CheckIfInventoryHasItems(Vector2[] AmountOfUniqueIds)
    {
        for (int i = 0; i < AmountOfUniqueIds.Length; i++)
        {
            if (CharacterOwner.Instance.Inventory.GetAmountOfItemsInDictionary((int)AmountOfUniqueIds[i].x) < (int)AmountOfUniqueIds[i].y)
            {
                return false;
            }
        }
        return true;
    }

    public static void RemoveItemsFromInventory(Vector2[] AmountOfUniqueIds)
    {
        for (int x = 0; x < AmountOfUniqueIds.Length; x++)
        {
            for (int y = 0; y < AmountOfUniqueIds[x].y; y++)
            {
                CharacterOwner.Instance.Inventory.RemoveItemFromInventoryWithId((int)AmountOfUniqueIds[x].x);
            }
        }
    }
}
