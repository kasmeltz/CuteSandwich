namespace HairyNerd.CuteSandwich.Unity.Behaviours.SandwichScene
{
    using System.Collections.Generic;
    using UnityEngine;

    public class SandwichRecipe
    {
        #region Recipes

        public static Dictionary<string, SandwichOrder> Recipes = new Dictionary<string, SandwichOrder>
        {
            ["Ham And Swiss"] = new SandwichOrder 
            {
                Parts = new List<SandwichPart>
                {
                    new SandwichPart(PartIngredient.WheatBread),
                    new SandwichPart(PartIngredient.Ham),
                    new SandwichPart(PartIngredient.SwissCheese),
                    new SandwichPart(PartIngredient.WhiteBread)                    
                }
            }

        };

        #endregion

        #region Public Methods

        public static SandwichOrder GetRandomOrder(
            List<PartIngredient> ingredientsAllowed,
            List<PartSauce> saucesAllowed)
        {
            List<SandwichOrder> potentialOrders = new List<SandwichOrder>();

            foreach(var recipe in Recipes.Values) 
            {
                bool isInvalidIngredient = false;
                foreach (var part in recipe.Parts)
                {
                    if (!ingredientsAllowed
                        .Contains(part.Ingredient))
                    {
                        isInvalidIngredient = true;
                        break;
                    }

                    if (part.Sauce != PartSauce.None)
                    {
                        if (!saucesAllowed
                            .Contains(part.Sauce))
                        {
                            isInvalidIngredient = true;
                            break;
                        }
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

        public static void SetBreadType(SandwichOrder order, PartIngredient breadType)
        {
            foreach (var part in order.Parts)
            {
                if (part.Ingredient == PartIngredient.WhiteBread)
                {
                    part.Ingredient = breadType;
                }
            }
        }

        #endregion
    }
}