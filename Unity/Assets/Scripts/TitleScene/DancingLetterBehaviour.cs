namespace HairyNerd.CuteSandwich.Unity.Behaviours.TitleScene
{
    using TMPro;
    using UnityEngine;

    [AddComponentMenu("HairyNerd/CuteSandwich/TitleScene/DancingLetter")]
    public class DancingLetterBehaviour : BehaviourBase
    {
        #region Members

        protected TMP_Text Text { get; set; }

        protected Vector2 InitialPosition { get; set; }

        protected float Speed { get; set; }

        protected int Direction { get; set; }


        #endregion

        #region Protected Methods

        protected void ChangeColor()
        {
            var r = Mathf.Min(1, Random.value * 2);
            var g = Mathf.Min(1, Random.value * 2);
            var b = Mathf.Min(1, Random.value * 2);

            Text.color = new Color(r, g, b);

            ExecuteAfterTime(Random.value * 0.5f, () =>
            {
                ChangeColor();
            });
        }

        #endregion

        #region Unity

        protected void Update()
        {
            Speed += Random.Range(50, 10000) * Direction * Time.deltaTime;

            var position = Text.rectTransform.anchoredPosition;
            position.y += Speed * Time.deltaTime;

            if (position.y > InitialPosition.y + 25)
            {
                position.y = InitialPosition.y + 25;
                Direction = -1;
            }
            else if (position.y < InitialPosition.y - 25)
            {
                position.y = InitialPosition.y - 25;
                Direction = 1;
            }

            Text.rectTransform.anchoredPosition = position;
        }

        protected override void Awake()
        {
            base
                .Awake();

            Text = GetComponent<TMP_Text>();
            ChangeColor();
            InitialPosition = Text.rectTransform.anchoredPosition;

            if (Random.value > 0.5f)
            {
                Direction = 1;
            }
            else
            {
                Direction = -1;
            }

            Speed = Random.Range(0, 50) * Direction;
        }

        #endregion
    }
}