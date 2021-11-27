namespace HairyNerd.CuteSandwich.Unity.Behaviours.SandwichScene
{
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("HairyNerd/CuteSandwich/SandwichScene/SandwichPart")]

    public class SandwichPartBehaviour : BehaviourBase
    {
        #region Members

        public RectTransform RectTransform;
        public Image Ingredient;
        public Image Sauce;
        public Image Mask;
        public Image Outline;

        public SandwichPart SandwichPart { get; set; }

        #endregion

        #region Public Methods

        public void SetSandwichPart(SandwichPart sandwichPart, bool useResult)
        {
            SandwichPart = sandwichPart;

            SetIngredient(sandwichPart.Ingredient);
            SetSauce(sandwichPart.Sauce);
            SetShape(sandwichPart.Shape);
        }

        public void SetSauce(PartSauce sauce)
        {
            if (sauce == PartSauce.None)
            {
                Sauce.gameObject.SetActive(false);
                return;
            }

            Sauce.gameObject.SetActive(true);

            var sprite = Resources
                   .Load<Sprite>($"Images/Sauce/{sauce}");

            if (sprite == null)
            {
                Debug.LogError($"CANT FIND IMAGE FOR SAUCE {sauce}");
            }
            else
            {
                Sauce.sprite = sprite;
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
                Ingredient.sprite = partSprite;
            }
        }

        public void SetShape(int shapeIndex)
        {
            if (shapeIndex < 0)
            {
                Ingredient.maskable = false;

                Outline
                    .gameObject
                    .SetActive(false);
            }
            else
            {
                var sprites = Resources
                    .LoadAll<Sprite>("Images/Shapes/all_shapes");

                Mask.sprite = sprites[shapeIndex];
                Ingredient.maskable = true;

                Outline
                    .gameObject
                    .SetActive(true);

                sprites = Resources
                   .LoadAll<Sprite>("Images/Shapes/all_shapes_outline");

                Outline.sprite = sprites[shapeIndex];
            }
        }

        #endregion
    }
}