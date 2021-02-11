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
        Vector2[] woodenAxeRequiredItems = new Vector2[] { new Vector2(6,4)};
        CraftingRecipe stoneAxe = new CraftingRecipe(3, woodenAxeRequiredItems, "This is an axe made out of wood.", true);
        _craftingRecipes.Add(stoneAxe);

        Vector2[] torchRequiredItems = new Vector2[] {new Vector2(6,3)};
        CraftingRecipe torch = new CraftingRecipe(8, torchRequiredItems, "This is a torch. It lights up shit.", true);
        _craftingRecipes.Add(torch);

        Vector2[] woodenPickaxeRequiredItems = new Vector2[] { new Vector2(6,6) };
        CraftingRecipe woodenPickaxe = new CraftingRecipe(2, woodenPickaxeRequiredItems, "This is a pickaxe. You can brake shit with it.", true);
        _craftingRecipes.Add(woodenPickaxe);

        Vector2[] flashlightRequiredItems = new Vector2[] {};
        CraftingRecipe flashlight = new CraftingRecipe(3, flashlightRequiredItems, "This is a light source. You can light up shit with it.");
        _craftingRecipes.Add(flashlight);

        Vector2[] buildingPlanRequiredItems = new Vector2[] { new Vector2(6,2)};
        CraftingRecipe buildingPlan = new CraftingRecipe(9, buildingPlanRequiredItems, "This is a building plan. You can build nice shitty buildings with it.", true);
        _craftingRecipes.Add(buildingPlan);
    }

}
