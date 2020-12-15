using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftManager : MonoBehaviour
{

    [SerializeField] private GameObject craftingRecipeSlot;
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
            slot.GetComponent<CraftSlot>().SlotRecipe = craftingRecipe;
            slot.transform.GetChild(0).GetComponent<Image>().sprite = item.Icon;
            slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.ItemName;
        }
    }

}
