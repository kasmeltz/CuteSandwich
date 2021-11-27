namespace HairyNerd.CuteSandwich.Unity.Behaviours.SandwichScene
{
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("HairyNerd/CuteSandwich/SandwichScene/SandwichPart")]

    public class SandwichPartBehaviour : BehaviourBase
    {
        #region Members

        public Image PartImage;
        public Image PartMask;

        public SandwichPart SandwichPart { get; set; }

        #endregion

        #region Public Methods

        public void SetSandwichPart(SandwichPart sandwichPart, bool useResult)
        {
            SandwichPart = sandwichPart;

            SetIngredient(sandwichPart.Ingredient);
            if (useResult)
            {
                SetShape(sandwichPart.ResultShape);
            }
            else
            {
                SetShape(sandwichPart.DesiredShape);
            }
        }

        public void SetIngredient(PartIngredient ingredient)
        {
            var partSprite = Resources
                   .Load<Sprite>($"Images/Ingredients/{ingredient}");

            if (partSprite == null)
            {
                Debug.LogError($"CANT FIND IMAGE FOR INGREDIENT {ingredient}");
            }
            else
            {
                PartImage.sprite = partSprite;
            }
        }

        public void SetShape(int shapeIndex)
        {
            if (shapeIndex < 0)
            {
                PartImage.maskable = false;
            }
            else
            {
                var sprites = Resources
                    .LoadAll<Sprite>("Images/Shapes/all_shapes");

                PartMask.sprite = sprites[shapeIndex];
                PartImage.maskable = true;
            }
        }

        #endregion
    }
}