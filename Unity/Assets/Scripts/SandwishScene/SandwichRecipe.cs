using System.Collections.Generic;
using UnityEngine;

namespace HairyNerd.CuteSandwich.Unity.Behaviours.SandwichScene
{
    public class SandwichRecipe
    {
        #region Recipes

        public static Dictionary<string, SandwichOrder> Recipes = new Dictionary<string, SandwichOrder>
        {
            ["HamAndMozzarella"] = new SandwichOrder(PartIngredient.WhiteBread, PartIngredient.HamPlain, PartIngredient.Mozzarella, PartIngredient.WhiteBread)           
        };

        #endregion

        #region Public Methods

        public static SandwichOrder GetRandomOrder(HashSet<PartIngredient> ingredientsAllowed)
        {
            List<SandwichOrder> potentialOrders = new List<SandwichOrder>();

            foreach(var recipe in Recipes.Values) 
            {
                bool isInvalidIngredient = false;
                foreach(var part in recipe.Parts)
                {
                    if (!ingredientsAllowed.Contains(part.Ingredient))
                    {
                        isInvalidIngredient = true;
                        break;
                    }
                }

                if (!isInvalidIngredient)
                {
                    potentialOrders
                        .Add(recipe);
                }
            }

            int index = Random.Range(0, potentialOrders.Count);

            var chosen = potentialOrders[index];

            return new SandwichOrder(chosen);
        }

        #endregion
    }
}