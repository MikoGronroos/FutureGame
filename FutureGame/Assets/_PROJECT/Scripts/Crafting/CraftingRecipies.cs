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
        Vector2[] stoneAxeRequireItems = new Vector2[2] { new Vector2(9,3), new Vector2(1,2)};
        CraftingRecipe stoneAxe = new CraftingRecipe(3, stoneAxeRequireItems, "This is an axe made of stone.", true);
        _craftingRecipes.Add(stoneAxe);

        Vector2[] torchRequiredItems = new Vector2[] {new Vector2(1,3)};
        CraftingRecipe torch = new CraftingRecipe(8, torchRequiredItems, "This is a torch. It lights up shit.", true);
        _craftingRecipes.Add(torch);

        Vector2[] woodenPickaxeRequiredItems = new Vector2[] { new Vector2(1,6) };
        CraftingRecipe woodenPickaxe = new CraftingRecipe(10, woodenPickaxeRequiredItems, "This is a pickaxe. You can brake shit with it.", true);
        _craftingRecipes.Add(woodenPickaxe);

        Vector2[] flashlightRequiredItems = new Vector2[] {};
        CraftingRecipe flashlight = new CraftingRecipe(5, flashlightRequiredItems, "This is a light source. You can light up shit with it.");
        _craftingRecipes.Add(flashlight);

        Vector2[] buildingPlanRequiredItems = new Vector2[] { new Vector2(1,2)};
        CraftingRecipe buildingPlan = new CraftingRecipe(13, buildingPlanRequiredItems, "This is a building plan. You can build nice shitty buildings with it.", true);
        _craftingRecipes.Add(buildingPlan);
    }

}
