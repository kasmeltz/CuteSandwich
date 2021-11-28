namespace HairyNerd.CuteSandwich.Unity.Behaviours.SandwichScene
{
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("HairyNerd/CuteSandwich/SandwichScene/SandwichOrderPanel")]

    public class SandwichOrderPanelBehaviour : BehaviourBase
    {
        #region Members

        public RectTransform IngredientPanel;
        public Image FaceImage;
        public SandwichPartBehaviour SandwichPartPrefab;

        public SandwichOrder SandwichOrder { get; set; }

        #endregion

        #region Public Methods
        
        public void RandomizeFace()
        {

            var sprites = Resources
                .LoadAll<Sprite>("IMages/Faces/faces_all");

            int index = Random.Range(0, sprites.Length);
            FaceImage.sprite = sprites[index];
        }

        public void SetSandwichOrder(SandwichOrder order)
        {
            SandwichOrder = order;

            foreach (var sandwichPart in order.Parts)
            {
                var sandwichPartBehaviour = Instantiate(SandwichPartPrefab);
                sandwichPartBehaviour
                    .SetSandwichPart(sandwichPart);

                sandwichPartBehaviour
                    .transform
                    .SetParent(IngredientPanel);
            }
        }

        #endregion
    }
}