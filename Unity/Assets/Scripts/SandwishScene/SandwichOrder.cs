namespace HairyNerd.CuteSandwich.Unity.Behaviours.SandwichScene
{
    using System.Collections.Generic;
    using System.Linq;

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
                        Sauce = part.Sauce,
                        Shape = part.Shape
                    });
            }
        }        

        #endregion

        #region Members

        public List<SandwichPart> Parts { get; set; }

        public float Score 
        { 
            get
            {
                float score = 0;

                score += Parts.Count * 0.4f;
                score += Parts.Count(o => o.Sauce != PartSauce.None) * 0.4f;
                score += 1.5f;

                return score;

            }
        }

        #endregion
    }
}