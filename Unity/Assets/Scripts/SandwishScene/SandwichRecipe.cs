namespace HairyNerd.CuteSandwich.Unity.Behaviours.SandwichScene
{
    using System.Collections.Generic;
    using UnityEngine;

    public class SandwichRecipe
    {
        #region Recipes

        public static Dictionary<string, SandwichOrder> Recipes = new Dictionary<string, SandwichOrder>
        {
            ["Ham And Swiss"] = new SandwichOrder(PartIngredient.WhiteBread, PartIngredient.Ham, PartIngredient.SwissCheese, PartIngredient.WhiteBread)           
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