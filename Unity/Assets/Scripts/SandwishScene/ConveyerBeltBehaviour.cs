namespace HairyNerd.CuteSandwich.Unity.Behaviours.SandwichScene
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("HairyNerd/CuteSandwich/SandwichScene/ConveyerBelt")]

    public class ConveyerBeltBehaviour : BehaviourBase
    {
        #region Members

        public float PartMoveSpeed = 200;
        public float PartCreatePoint = -200;
        public Image ShapeImage;

        public SandwichPartBehaviour SandwichPartPrefab;

        public RectTransform CreatedParts;

        protected PartShape SelectedShape { get; set; }

        public List<SandwichPart> PartsToCreate { get; set; }

        public List<SandwichOrder> SandwichOrders { get; set; }

        public List<SandwichPartBehaviour> SandwichParts { get; set; }

        #endregion

        #region Public Methods

        public void MakeOrders()
        {
            int orderCount = 2;

            for(int i = 0;i < orderCount;i++)
            {
                var order = new SandwichOrder();

                var part = new SandwichPart
                {
                    Ingredient = PartIngredient.WhiteBread,
                    DesiredShape = PartShape.Heart,
                    ResultShape = PartShape.None
                };

                order
                    .Parts
                    .Add(part);

                part = new SandwichPart
                {
                    Ingredient = PartIngredient.HamPlain,
                    DesiredShape = PartShape.Heart,
                    ResultShape = PartShape.None
                };

                order
                    .Parts
                    .Add(part);

                part = new SandwichPart
                {
                    Ingredient = PartIngredient.Mozzarella,
                    DesiredShape = PartShape.Heart,
                    ResultShape = PartShape.None
                };

                order
                    .Parts
                    .Add(part);

                part = new SandwichPart
                {
                    Ingredient = PartIngredient.WhiteBread,
                    DesiredShape = PartShape.Heart,
                    ResultShape = PartShape.None
                };

                order
                    .Parts
                    .Add(part);

                SandwichOrders
                    .Add(order);
            }
            
            foreach(var order in SandwichOrders)
            {
                foreach(var part in order.Parts)
                {
                    PartsToCreate
                        .Add(part);
                }
            }
        }

        public void RotateShape(int direction)
        {
            int currentShape = (int)SelectedShape;
            currentShape += direction;
            if (currentShape < 1)
            {
                currentShape = 2;
            }
            else if (currentShape > 2)
            {
                currentShape = 1;
            }

            SetSelectedShape((PartShape)currentShape);
        }

        public void SetSelectedShape(PartShape shape)
        {
            SelectedShape = shape;
            ShapeImage.sprite = Resources
                .Load<Sprite>($"Images/Shapes/{SelectedShape}");
        }

        #endregion

        #region Protected Methods

        protected void MakeNewParts()
        {
            if (!PartsToCreate.Any())
            {
                return;
            }

            bool createPart = false;

            var lastPart = SandwichParts
                .LastOrDefault();

            if (lastPart == null)
            {
                createPart = true;
            }
            else
            {
                if (lastPart.PartMask.rectTransform.anchoredPosition.x >= PartCreatePoint)
                {
                    createPart = true;
                }
            }

            if (createPart)
            {
                var partIndex = Random
                    .Range(0, PartsToCreate.Count);

                var part = Instantiate(SandwichPartPrefab);
                
                part.SandwichPart = PartsToCreate[partIndex];

                part
                    .transform
                    .SetParent(transform);

                part.PartMask.rectTransform.anchoredPosition = new Vector2(-800, 0);

                var partSprite = Resources
                    .Load<Sprite>($"Images/Ingredients/{part.SandwichPart.Ingredient}");

                if (partSprite == null)
                {
                    Debug.LogError($"CANT FIND IMAGE FOR INGREDIENT {part.SandwichPart.Ingredient}");
                }
                else 
                { 
                    part.PartImage.sprite = partSprite;
                }

                part.PartImage.maskable = false;
                
                SandwichParts
                    .Add(part);

                PartsToCreate
                    .RemoveAt(partIndex);
            }
        }        

        protected void MoveConveyer()
        {
            MakeNewParts();

            Vector2 moveDirection = new Vector3(Time.deltaTime * PartMoveSpeed, 0);

            foreach (var part in SandwichParts)
            {
                if (part.transform.parent != CreatedParts)
                {
                    part.PartMask.rectTransform.anchoredPosition += moveDirection;

                    if (part.PartMask.rectTransform.anchoredPosition.x >= 0)
                    {
                        if (!part.PartImage.maskable)
                        {
                            var maskSprite = Resources
                                .Load<Sprite>($"Images/Shapes/{SelectedShape}");

                            if (maskSprite == null)
                            {
                                Debug.LogError($"CANT FIND IMAGE FOR SHAPE {SelectedShape}");
                            }
                            else
                            {
                                part.SandwichPart.ResultShape = SelectedShape;
                                part.PartMask.sprite = maskSprite;
                                part.PartImage.maskable = true;
                            }
                        }
                    }

                    if (part.PartMask.rectTransform.anchoredPosition.x >= 700)
                    {
                        part.PartMask.rectTransform.anchoredPosition = new Vector2(0, 0);
                        part
                            .transform
                            .SetParent(CreatedParts);
                    }
                }
            }

            var lastPart = SandwichParts
                .LastOrDefault();

            if (!PartsToCreate.Any())
            {
                if (lastPart.PartMask.rectTransform.anchoredPosition.x >= 700)
                {
                    Debug.Log("CHECK SANDWICH PARTS!");
                }
            }
        }

        #endregion

        #region Unity

        protected void Update()
        {
            MoveConveyer();

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                RotateShape(-1);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                RotateShape(1);
            }
        }

        protected override void Awake()
        {
            base
                .Awake();

            PartsToCreate = new List<SandwichPart>();
            SandwichOrders = new List<SandwichOrder>();
            SandwichParts = new List<SandwichPartBehaviour>();

            SetSelectedShape(PartShape.Heart);

            MakeOrders();
        }

        #endregion
    }
}