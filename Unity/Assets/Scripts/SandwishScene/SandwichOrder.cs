namespace HairyNerd.CuteSandwich.Unity.Behaviours.SandwichScene
{
    using System.Collections.Generic;

    public class SandwichOrder
    {
        #region Constructors

        public SandwichOrder()
        {
            Parts = new List<SandwichPart>();
        }

        public SandwichOrder(SandwichOrder clone)
        {
            Parts = new List<SandwichPart>(clone.Parts.Count);
            foreach (var part in clone.Parts)
            {
                Parts
                    .Add(new SandwichPart
                    {
                        Ingredient = part.Ingredient,
                        ResultShape = part.ResultShape,
                        DesiredShape = part.DesiredShape
                    });
            }
        }        

        #endregion

        #region Members

        public List<SandwichPart> Parts { get; set; }

        #endregion
    }
}