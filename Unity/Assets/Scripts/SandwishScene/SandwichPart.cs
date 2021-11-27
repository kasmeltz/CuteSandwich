namespace HairyNerd.CuteSandwich.Unity.Behaviours.SandwichScene
{
    public class SandwichPart
    {
        #region Constructor

        public SandwichPart()
        {
            Shape = -1;
            Sauce = PartSauce.None;
        }

        public SandwichPart(PartIngredient ingredient)
        {
            Ingredient = ingredient;
            Shape = -1;
            Sauce = PartSauce.None;
        }

        public SandwichPart(PartIngredient ingredient, int shape)
        {
            Ingredient = ingredient;
            Shape = shape;
            Sauce = PartSauce.None;
        }


        public SandwichPart(PartIngredient ingredient, PartSauce sauce)
        {
            Ingredient = ingredient;
            Shape = -1;
            Sauce = sauce;
        }

        #endregion

        #region Members

        public int Shape { get; set; }

        public PartIngredient Ingredient { get; set; }

        public PartSauce Sauce { get; set;}

        #endregion
    }
}