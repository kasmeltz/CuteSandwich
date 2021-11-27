namespace HairyNerd.CuteSandwich.Unity.Behaviours
{
    using UnityEngine;

    public abstract class BehaviourBase : MonoBehaviour
    {
        #region Protected Methods

        protected void MegaDestroy(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }

            Destroy(gameObject);
        }

        #endregion

        #region Unity

        protected virtual void Awake()
        {

        }

        #endregion
    }
}