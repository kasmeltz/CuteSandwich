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
        public int MaxShapeIndex = 4;
        public int OrdersToCreate = 1;

        public SandwichOrderPanelBehaviour OrderPanelPrefab;

        public SandwichPartBehaviour SandwichPartPrefab;
        
        public RectTransform OrderContainer;
        public RectTransform CreatedParts;

        protected int SelectedShapeIndex { get; set; }

        public List<SandwichPart> PartsToCreate { get; set; }

        public List<SandwichOrder> SandwichOrders { get; set; }

        public List<SandwichPartBehaviour> RequestedParts { get; set; }

        public List<SandwichPartBehaviour> SandwichParts { get; set; }

        #endregion

        #region Public Methods

        public void MakeOrders()
        {
            for(int i = 0;i < OrdersToCreate; i++)
            {
                var orderPanel = Instantiate(OrderPanelPrefab);

                var order = new SandwichOrder();

                order
                    .Parts
                    .Add(new SandwichPart
                    {
                        Ingredient = PartIngredient.WhiteBread,
                        DesiredShape = Random.Range(0, MaxShapeIndex)
                    });

                order
                    .Parts
                    .Add(new SandwichPart
                    {
                        Ingredient = PartIngredient.Mozzarella,
                        DesiredShape = Random.Range(0, MaxShapeIndex)
                    });

                order
                    .Parts
                    .Add(new SandwichPart
                    {
                        Ingredient = PartIngredient.HamPlain,
                        DesiredShape = Random.Range(0, MaxShapeIndex)
                    });

                order
                   .Parts
                   .Add(new SandwichPart
                   {
                       Ingredient = PartIngredient.WhiteBread,
                       DesiredShape = Random.Range(0, MaxShapeIndex)
                   });

                orderPanel
                    .SetSandwichOrder(order);

                orderPanel
                    .transform
                    .SetParent(OrderContainer);

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
            SelectedShapeIndex += direction;
            if (SelectedShapeIndex < 0)
            {
                SelectedShapeIndex = MaxShapeIndex - 1;
            }
            else if (SelectedShapeIndex >= MaxShapeIndex)
            {
                SelectedShapeIndex = 0;
            }

            SetSelectedShape(SelectedShapeIndex);
        }

        public void SetSelectedShape(int shapeIndex)
        {
            SelectedShapeIndex = shapeIndex;

            var sprites = Resources
                .LoadAll<Sprite>("Images/Shapes/all_shapes");

            ShapeImage.sprite = sprites[shapeIndex];
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
                
                part
                    .transform
                    .SetParent(transform);

                part.PartMask.rectTransform.anchoredPosition = new Vector2(-800, 0);

                part
                    .SetSandwichPart(PartsToCreate[partIndex], true);

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
                            part
                                .SetShape(SelectedShapeIndex);
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
            RequestedParts = new List<SandwichPartBehaviour>();
            SandwichParts = new List<SandwichPartBehaviour>();

            SetSelectedShape(0);

            MakeOrders();
        }

        #endregion
    }
}