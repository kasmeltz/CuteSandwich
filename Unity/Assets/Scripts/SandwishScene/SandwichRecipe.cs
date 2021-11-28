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
                    new SandwichPart(PartIngredient.WhiteBread),
                    new SandwichPart(PartIngredient.Ham),
                    new SandwichPart(PartIngredient.SwissCheese),
                    new SandwichPart(PartIngredient.WhiteBread)                    
                }
            },
            ["Grilled Cheese"] = new SandwichOrder
            {
                Parts = new List<SandwichPart>
                {
                    new SandwichPart(PartIngredient.WhiteBread),
                    new SandwichPart(PartIngredient.Cheddar),
                    new SandwichPart(PartIngredient.WhiteBread)
                }
            },
            ["Meatball"] = new SandwichOrder
            {
                Parts = new List<SandwichPart>
                {
                    new SandwichPart(PartIngredient.ItalianBread),
                    new SandwichPart(PartIngredient.Meatballs),
                    new SandwichPart(PartIngredient.Provolone, PartSauce.Tomato),
                    new SandwichPart(PartIngredient.ItalianBread),
                }
            },
            ["BLT"] = new SandwichOrder
            {
                Parts = new List<SandwichPart>
                {
                    new SandwichPart(PartIngredient.WhiteBread, PartSauce.Mayonnaise),
                    new SandwichPart(PartIngredient.Bacon),
                    new SandwichPart(PartIngredient.Lettuce),
                    new SandwichPart(PartIngredient.Tomato),                    
                    new SandwichPart(PartIngredient.WhiteBread),
                }
            },
            ["Peanut Butter and Jelly"] = new SandwichOrder
            {
                Parts = new List<SandwichPart>
                {
                    new SandwichPart(PartIngredient.WhiteBread, PartSauce.PeanutButter),
                    new SandwichPart(PartIngredient.WhiteBread, PartSauce.Jelly)
                }
            },
            ["Roast Beef"] = new SandwichOrder
            {
                Parts = new List<SandwichPart>
                {
                    new SandwichPart(PartIngredient.WhiteBread),
                    new SandwichPart(PartIngredient.RoastBeef, PartSauce.DijonMustard),
                    new SandwichPart(PartIngredient.Provolone),
                    new SandwichPart(PartIngredient.RedOnion),                    
                    new SandwichPart(PartIngredient.WhiteBread),
                }
            },
            ["Tuna"] = new SandwichOrder
            {
                Parts = new List<SandwichPart>
                {
                    new SandwichPart(PartIngredient.WhiteBread, PartSauce.Mayonnaise),
                    new SandwichPart(PartIngredient.Tuna),
                    new SandwichPart(PartIngredient.Lettuce),
                    new SandwichPart(PartIngredient.Tomato),
                    new SandwichPart(PartIngredient.WhiteBread),
                }
            },
            ["Turkey"] = new SandwichOrder
            {
                Parts = new List<SandwichPart>
                {
                    new SandwichPart(PartIngredient.WhiteBread, PartSauce.Mayonnaise),
                    new SandwichPart(PartIngredient.Turkey),
                    new SandwichPart(PartIngredient.Lettuce),
                    new SandwichPart(PartIngredient.Tomato),
                    new SandwichPart(PartIngredient.WhiteBread),
                }
            },
            ["Grilled Chicken"] = new SandwichOrder
            {
                Parts = new List<SandwichPart>
                {
                    new SandwichPart(PartIngredient.WhiteBread, PartSauce.Mayonnaise),
                    new SandwichPart(PartIngredient.Chicken),
                    new SandwichPart(PartIngredient.SwissCheese),
                    new SandwichPart(PartIngredient.Lettuce),
                    new SandwichPart(PartIngredient.Tomato),
                    new SandwichPart(PartIngredient.WhiteBread, PartSauce.DijonMustard),
                }
            },
            ["Club"] = new SandwichOrder
            {
                Parts = new List<SandwichPart>
                {
                    new SandwichPart(PartIngredient.WhiteBread, PartSauce.Mayonnaise),
                    new SandwichPart(PartIngredient.Turkey),
                    new SandwichPart(PartIngredient.Ham),
                    new SandwichPart(PartIngredient.WhiteBread, PartSauce.Mayonnaise),                    
                    new SandwichPart(PartIngredient.Bacon),
                    new SandwichPart(PartIngredient.Cheddar),
                    new SandwichPart(PartIngredient.Lettuce),
                    new SandwichPart(PartIngredient.Tomato),
                    new SandwichPart(PartIngredient.WhiteBread, PartSauce.DijonMustard),
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