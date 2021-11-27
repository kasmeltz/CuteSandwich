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

        public void SetShape(PartShape shape)
        {
            if (shape == PartShape.None)
            {
                PartImage.maskable = false;
            }
            else
            {
                var maskSprite = Resources
                               .Load<Sprite>($"Images/Shapes/{shape}");

                if (maskSprite == null)
                {
                    Debug.LogError($"CANT FIND IMAGE FOR SHAPE {shape}");
                }
                else
                {
                    PartMask.sprite = maskSprite;
                    PartImage.maskable = true;
                }
            }
        }

        #endregion
    }
}