namespace HairyNerd.CuteSandwich.Unity.Behaviours.SandwichScene
{
    public class SandwichPart
    {
        #region Constructor

        public SandwichPart()
        {
            ResultShape = -1;
        }

        #endregion

        #region Members

        public int DesiredShape { get; set; }

        public int ResultShape { get; set; }

        public PartIngredient Ingredient { get; set; }

        public PartSauce Sauce { get; set;}

        #endregion
    }
}