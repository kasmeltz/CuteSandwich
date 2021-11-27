namespace HairyNerd.CuteSandwich.Unity.Behaviours.SandwichScene
{
    using System.Collections.Generic;
    using System.Linq;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("HairyNerd/CuteSandwich/SandwichScene/ConveyerBelt")]

    public class ConveyerBeltBehaviour : BehaviourBase
    {
        #region Members

        public TMP_Text ScoreText;
        public float BasePartMoveSpeed = 100;
        public float PartCreatePoint = -100;
        public float PartMoveSpeedPerLevel = 10;        
        public Image ShapeImage;
        public Image IngredientImage;      
        public int OrdersToCreate = 1;             

        public SandwichOrderPanelBehaviour OrderPanelPrefab;

        public SandwichPartBehaviour SandwichPartPrefab;
        
        public RectTransform OrderContainer;
        public RectTransform CreatedParts;

        public int SelectedShapeIndex { get; set; }

        public int SelectedIngredientIndex { get; set; }

        public List<SandwichOrder> SandwichOrders { get; set; }

        public List<SandwichPartBehaviour> AvailableParts { get; set; }

        public List<SandwichPartBehaviour> RequestedParts { get; set; }

        public List<SandwichPartBehaviour> SandwichParts { get; set; }        

        public SandwichSceneState SandwichSceneState { get; set; }

        protected int TotalPossibleShapes { get; set; }
        
        public Queue<PartIngredient> IngredientsToAdd { get; set; }

        public List<PartIngredient> IngredientsAllowed { get; set; }

        public float Score { get; set; }

        public float ScoreToAdd { get; set; }

        public int MaxShapeIndex { get; set; }

        public int Level { get; set; }

        public float PartMoveSpeed { get; set; }

        #endregion

        #region Public Methods

        public void MakeOrders()
        {
            for(int i = 0;i < OrdersToCreate; i++)
            {
                var orderPanel = Instantiate(OrderPanelPrefab);

                var order = SandwichRecipe
                    .GetRandomOrder(IngredientsAllowed);

                foreach(var part in order.Parts)
                {
                    part.DesiredShape = Random.Range(0, MaxShapeIndex);
                }

                orderPanel
                    .SetSandwichOrder(order);

                orderPanel
                    .transform
                    .SetParent(OrderContainer);

                SandwichOrders
                    .Add(order);
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
                .LoadAll<Sprite>("Images/Shapes/all_shapes_outline");

            TotalPossibleShapes = sprites.Length;

            ShapeImage.sprite = sprites[shapeIndex];
        }

        public void RotateIngredient(int direction)
        {
            SelectedIngredientIndex += direction;
            if (SelectedIngredientIndex < 0)
            {
                SelectedIngredientIndex = IngredientsAllowed.Count - 1;
            }
            else if (SelectedIngredientIndex >= IngredientsAllowed.Count)
            {
                SelectedIngredientIndex = 0;
            }

            SetSelectedIngredient(SelectedIngredientIndex);
        }

        public void SetSelectedIngredient(int ingredientIndex)
        {
            SelectedIngredientIndex = ingredientIndex;

            var ingredient = IngredientsAllowed[SelectedIngredientIndex];

            IngredientImage.sprite = Resources
                .Load<Sprite>($"Images/Ingredients/{ingredient}");
        }

        public void MakePart()
        {
            var lastPart = SandwichParts
               .LastOrDefault();

            if (lastPart != null &&
                lastPart.RectTransform.anchoredPosition.x <= -700)
            {
                return;
            }

            var part = Instantiate(SandwichPartPrefab);

            part
                .transform
                .SetParent(transform);

            part.RectTransform.anchoredPosition = new Vector2(-700, 0);

            var ingredient = IngredientsAllowed[SelectedIngredientIndex];

            var sandwichPart = new SandwichPart
            {
                Ingredient = ingredient,
                ResultShape = -1,
                DesiredShape = -1
            };

            part
                .SetSandwichPart(sandwichPart, true);

            part.Mask.maskable = false;

            SandwichParts
                .Add(part);

            SetScore(Score - 1);

            if (Score < 0)
            {
                SandwichSceneState = SandwichSceneState.GameOver;
            }
        }

        public void SetScore(float score)
        {
            Score = score;
            ScoreText.text = $"{Mathf.RoundToInt(Score)}";
        }

        public void SetLevel(int level)
        {
            Level = level;
            if (TotalPossibleShapes == 0 ||
                MaxShapeIndex < TotalPossibleShapes)
            {
                MaxShapeIndex = (Level / 2) + 4;
            }

            PartMoveSpeed = BasePartMoveSpeed + (PartMoveSpeedPerLevel * Level);
        }


        #endregion

        #region Protected Methods

        protected void MoveConveyer()
        {
            Vector2 moveDirection = new Vector3(Time.deltaTime * PartMoveSpeed, 0);

            foreach (var part in SandwichParts)
            {
                if (part.transform.parent != CreatedParts)
                {
                    part.RectTransform.anchoredPosition += moveDirection;

                    if (part.SandwichPart.ResultShape < 0 && 
                        part.RectTransform.anchoredPosition.x >= 0)
                    {
                        part.SandwichPart.ResultShape = SelectedShapeIndex;

                        part
                            .SetShape(SelectedShapeIndex);
                    }

                    if (part.RectTransform.anchoredPosition.x >= 700)
                    {
                        part.RectTransform.anchoredPosition = new Vector2(0, 0);
                        part
                            .transform
                            .SetParent(CreatedParts);

                        CheckSandwichCompleted();
                    }
                }
            }
        }

        protected void CheckSandwichCompleted()
        {
            AvailableParts
                .Clear();

            foreach (var sandwichPartBehaviour in SandwichParts)
            {
                AvailableParts
                    .Add(sandwichPartBehaviour);
            }

            int sandwichesCompleted = 0;
            foreach (var order in SandwichOrders)
            {
                int partsFound = 0;
                foreach (var part in order.Parts)
                {
                    var availablePart = AvailableParts.FirstOrDefault(o =>
                        o.transform.parent == CreatedParts &&
                        o.SandwichPart.Ingredient == part.Ingredient &&
                        o.SandwichPart.ResultShape == part.DesiredShape);

                    if (availablePart != null)
                    {
                        partsFound++;
                        AvailableParts
                            .Remove(availablePart);
                    }
                }

                if (partsFound == order.Parts.Count)
                {
                    sandwichesCompleted++;
                }
            }

            if (sandwichesCompleted == OrdersToCreate)
            {
                ScoreToAdd += 0.25f * SandwichOrders.Sum(o => o.Parts.Count);
                ScoreToAdd += 4;
                SandwichSceneState = SandwichSceneState.CalculatingScore;
            }
        }

        protected void NextLevel()
        {
            Reset();
            SetLevel(Level + 1);
            Level++;

            if (IngredientsToAdd
                .Any())
            {
                var ingredient = IngredientsToAdd
                    .Dequeue();

                IngredientsAllowed
                    .Add(ingredient);
            }

            SetSelectedShape(0);
            MakeOrders();

            SandwichSceneState = SandwichSceneState.MakingSandwiches;
        }

        protected void Reset()
        {
            foreach(var part in SandwichParts)
            {
                MegaDestroy(part.gameObject);
            }

            SandwichParts
                .Clear();
            
            foreach (var part in RequestedParts)
            {
                MegaDestroy(part.gameObject);
            }

            RequestedParts
                .Clear();

            SandwichOrders
                .Clear();
            
            foreach(Transform item in OrderContainer)
            {
                MegaDestroy(item.gameObject);
            }

            foreach (Transform item in CreatedParts)
            {
                MegaDestroy(item.gameObject);
            }
        }

        protected void UpdateMakingSandwiches()
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

            if (Input.GetKeyDown(KeyCode.Q))
            {
                RotateIngredient(-1);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                RotateIngredient(1);
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                MakePart();
            }
        }

        protected void UpdateCalculatingScore()
        {
            if (ScoreToAdd > 0)
            {
                var amount = Time.deltaTime * 20 * (Level + 1);
                if (amount > ScoreToAdd)
                {
                    amount = ScoreToAdd;
                }

                ScoreToAdd -= amount;
                SetScore(Score + amount);

                if (ScoreToAdd <= 0)
                {
                    ExecuteAfterTime(2, () =>
                    {
                        NextLevel();
                    });
                }
            }
        }

        protected void UpdateGameOver()
        {

        }

        #endregion

        #region Unity

        protected void Update()
        {
            switch (SandwichSceneState)
            {
                case SandwichSceneState.MakingSandwiches:
                    UpdateMakingSandwiches();
                    break;

                case SandwichSceneState.CalculatingScore:
                    UpdateCalculatingScore();
                    break;
                
                case SandwichSceneState.GameOver:
                    UpdateGameOver();
                    break;
            }

        }

        protected override void Awake()
        {
            base
                .Awake();
            
            SelectedIngredientIndex = 0;

            SandwichSceneState = SandwichSceneState.MakingSandwiches;

            SetLevel(0);
            SetScore(10);

            IngredientsAllowed = new List<PartIngredient>
            {
                PartIngredient.WhiteBread, PartIngredient.Ham, PartIngredient.SwissCheese, PartIngredient.Bacon
            };

            IngredientsToAdd = new Queue<PartIngredient>();

            IngredientsToAdd
                .Enqueue(PartIngredient.Tomato);

            IngredientsToAdd
                .Enqueue(PartIngredient.Lettuce);

            AvailableParts = new List<SandwichPartBehaviour>();
            SandwichOrders = new List<SandwichOrder>();
            RequestedParts = new List<SandwichPartBehaviour>();
            SandwichParts = new List<SandwichPartBehaviour>();

            SetSelectedShape(0);

            MakeOrders();
        }

        #endregion
    }
}