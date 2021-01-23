using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CraftManager : MonoBehaviour
{

    [SerializeField] private GameObject craftingRecipeSlot;
    [SerializeField] private GameObject neededItemSlot;
    [SerializeField] private Transform slotParent;

    private CraftingRecipies _recipies;

    private void Awake()
    {
        _recipies = GetComponent<CraftingRecipies>();
    }

    private void Start()
    {
        RefreshRecipies();
    }

    private void RefreshRecipies()
    {
        for (int i = 0; i < _recipies.CraftingRecipes.Count; i++)
        {
            CraftingRecipe craftingRecipe = _recipies.CraftingRecipes[i];
            Item item = ItemDictionary.Instance.GetItemByID(craftingRecipe.FinalItemId);
            GameObject slot = Instantiate(craftingRecipeSlot, slotParent);
            Transform recipeItemParent = slot.transform.Find("RecipeItemPanel");
            CraftSlot craftSlot = slot.GetComponent<CraftSlot>();
            craftSlot.SlotRecipe = craftingRecipe;
            craftSlot.AmountOfUniqueIds = craftingRecipe.ItemsNeeded;
            slot.transform.GetChild(0).GetComponent<Image>().sprite = item.Icon;
            slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.ItemName;
            for (int j = 0; j < craftSlot.AmountOfUniqueIds.Length; j++)
            {
                GameObject neededItem = Instantiate(neededItemSlot, recipeItemParent);
                string text = $"{craftSlot.AmountOfUniqueIds[j].y}x";
                neededItem.GetComponent<Image>().sprite = ItemDictionary.Instance.GetItemByID((int)craftSlot.AmountOfUniqueIds[j].x).Icon;
                neededItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
            }
        }
    }

    //x is item id and y is amount of items
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
