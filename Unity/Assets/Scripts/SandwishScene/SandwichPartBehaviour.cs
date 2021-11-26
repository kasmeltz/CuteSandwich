namespace HairyNerd.CuteSandwich.Unity.Behaviours.SandwichScene
{
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("HairyNerd/CuteSandwich/SandwichScene/SandwichPart")]

    public class SandwichPartBehaviour : BehaviourBase
    {
        public Image PartImage;
        public Mask PartMask;

        public SandwichPart SandwichPart { get; set; }
    }
}