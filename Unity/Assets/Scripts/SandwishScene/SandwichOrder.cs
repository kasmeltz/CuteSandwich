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

        #endregion

        #region Members

        public List<SandwichPart> Parts { get; set; }

        #endregion
    }
}