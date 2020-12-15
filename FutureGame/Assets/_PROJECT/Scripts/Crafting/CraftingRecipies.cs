using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipies : MonoBehaviour
{

    private List<CraftingRecipe> _craftingRecipes = new List<CraftingRecipe>();

    public List<CraftingRecipe> CraftingRecipes { get { return _craftingRecipes; } }

    public void Awake()
    {
        InitializeRecipes();
    }

    private void InitializeRecipes()
    {
        int[] stoneAxeRequireItems = new int[5] { 9,9,9,1,1};
        CraftingRecipe stoneAxe = new CraftingRecipe(3, stoneAxeRequireItems, "This is an axe made of stone.");
        _craftingRecipes.Add(stoneAxe);

        int[] torchRequiredItems = new int[3] {1, 1, 1 };
        CraftingRecipe torch = new CraftingRecipe(8, torchRequiredItems, "This is a torch. It lights up shit.");
        _craftingRecipes.Add(torch);

        int[] woodenPickaxeRequiredItems = new int[6] { 1, 1, 1, 1, 1, 1 };
        CraftingRecipe woodenPickaxe = new CraftingRecipe(10, woodenPickaxeRequiredItems, "This is a pickaxe. You can brake shit with it.");
        _craftingRecipes.Add(woodenPickaxe);

        int[] flashlightRequiredItems = new int[] {};
        CraftingRecipe flashlight = new CraftingRecipe(5, flashlightRequiredItems, "This is a light source. You can light up shit with it.");
        _craftingRecipes.Add(flashlight);

        int[] buildingPlanRequiredItems = new int[] { 1, 1};
        CraftingRecipe buildingPlan = new CraftingRecipe(15, buildingPlanRequiredItems, "This is a building plan. You can build nice shitty buildings with it.");
        _craftingRecipes.Add(buildingPlan);
    }

}
