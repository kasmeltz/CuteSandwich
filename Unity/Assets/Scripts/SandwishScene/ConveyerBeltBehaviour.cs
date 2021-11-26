namespace HairyNerd.CuteSandwich.Unity.Behaviours.SandwichScene
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [AddComponentMenu("HairyNerd/CuteSandwich/SandwichScene/ConveyerBelt")]

    public class ConveyerBeltBehaviour : BehaviourBase
    {
        #region Members

        public float PartMoveSpeed = 200;
        public float PartCreatePoint = -200;

        public SandwichPartBehaviour SandwichPartPrefab;

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
                    PartIngredient = PartIngredient.WhiteBread,
                    PartShape = PartShape.Heart
                };

                order
                    .Parts
                    .Add(part);

                part = new SandwichPart
                {
                    PartIngredient = PartIngredient.HamPlain,
                    PartShape = PartShape.Heart
                };

                order
                    .Parts
                    .Add(part);

                part = new SandwichPart
                {
                    PartIngredient = PartIngredient.Mozzarella,
                    PartShape = PartShape.Heart
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
                part.PartImage.maskable = false;
                part.SandwichPart = PartsToCreate[partIndex];

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
                part.PartMask.rectTransform.anchoredPosition += moveDirection;

                if (part.PartMask.rectTransform.anchoredPosition.x >= 0)
                {
                    if (!part.PartImage.maskable)
                    {
                        var maskSprite = Resources
                            .Load<Sprite>($"Images/Shapes/{part.SandwichPart.PartShape}");

                        part.PartMask.sprite = maskSprite;
                        part.PartImage.maskable = true;
                    }
                }
            }
        }

        #endregion

        #region Unity

        protected void Update()
        {
            MoveConveyer();                     
        }

        protected override void Awake()
        {
            base
                .Awake();

            PartsToCreate = new List<SandwichPart>();
            SandwichOrders = new List<SandwichOrder>();
            SandwichParts = new List<SandwichPartBehaviour>();

            MakeOrders();
        }

        #endregion
    }
}