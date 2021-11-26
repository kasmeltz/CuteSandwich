namespace HairyNerd.CuteSandwich.Unity.Behaviours.SandwichScene
{
    using System.Collections.Generic;
    using UnityEngine;

    [AddComponentMenu("HairyNerd/CuteSandwich/SandwichScene/ConveyerBelt")]

    public class ConveyerBeltBehaviour : BehaviourBase
    {
        #region Members

        public List<SandwichOrder> SandwichOrders { get; set; }

        public List<SandwichPartBehaviour> SandwichParts { get; set; }

        #endregion

        #region Unity

        protected override void Awake()
        {
            base
                .Awake();

            SandwichOrders = new List<SandwichOrder>();
            SandwichParts = new List<SandwichPartBehaviour>();
        }

        #endregion
    }
}