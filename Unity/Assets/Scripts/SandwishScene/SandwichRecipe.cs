namespace HairyNerd.CuteSandwich.Unity.Behaviours.SandwichScene
{
    using System.Collections.Generic;
    using UnityEngine;

    public class SandwichRecipe
    {
        #region Recipes

        public static Dictionary<string, SandwichOrder> Recipes = new Dictionary<string, SandwichOrder>
        {
            ["Ham And Swiss"] = new SandwichOrder(PartIngredient.WhiteBread, PartIngredient.Ham, PartIngredient.SwissCheese, PartIngredient.WhiteBread),
            ["BLT"] = new SandwichOrder(PartIngredient.WhiteBread, PartIngredient.Bacon, PartIngredient.Lettuce, PartIngredient.Tomato, PartIngredient.WhiteBread)
            /*
             * 1.	Grilled Cheese
a.	Bread
b.	Butter
c.	Cheddar cheese
2.	Ham and cheese
a.	Bread
b.	Ham
c.	Swiss cheese
3.	Meatball
a.	Italian bread
b.	Meatballs
c.	Tomato sauce
d.	Provolone cheese
4.	BLT
a.	Bread
b.	Bacon
c.	Lettuce
d.	Tomato
e.	Mayonnaise
5.	Peanut Butter and Jelly
a.	Bread
b.	Butter
c.	Peanut Butter
d.	Strawberry Jam
6.	Roast beef
a.	Bread
b.	Roastbeef
c.	Red Onion
d.	Lettuce
e.	Tomato
f.	Dijon mustard
7.	Tuna
a.	Bread
b.	Tuna
c.	Mayonnaise
d.	Lettuce
e.	Tomato
8.	Turkey
a.	Bread
b.	Turkey
c.	Mayonnaise
d.	Lettuce
e.	Tomato
9.	Grilled chicken
a.	Bread
b.	Grilled Chicken
c.	Mayonnaise
d.	Dijon Mustard
e.	Lettuce
f.	Tomato
10.	Club 
a.	Bread
b.	Turkey
c.	Ham
d.	Bread
e.	Bacon
f.	Mayonnaise
g.	Dijon Mustard
h.	Lettuce
i.	Tomato

            */
        };

        #endregion

        #region Public Methods

        public static SandwichOrder GetRandomOrder(List<PartIngredient> ingredientsAllowed)
        {
            List<SandwichOrder> potentialOrders = new List<SandwichOrder>();

            foreach(var recipe in Recipes.Values) 
            {
                bool isInvalidIngredient = false;
                foreach(var part in recipe.Parts)
                {
                    if (!ingredientsAllowed
                        .Contains(part.Ingredient))
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