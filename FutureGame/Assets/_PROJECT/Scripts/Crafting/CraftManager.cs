using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

            if (!craftingRecipe.IsDefaultRecipe) continue;

            Item item = ItemDictionary.Instance.GetItemByID(craftingRecipe.FinalItemId);

            if (item == null) continue;

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
}
