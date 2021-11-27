namespace HairyNerd.CuteSandwich.Unity.Behaviours
{
    using System;
    using System.Collections;
    using UnityEngine;

    public abstract class BehaviourBase : MonoBehaviour
    {
        #region Timers

        public Coroutine ExecuteAfterTime(float seconds, Action action)
        {
            var coroutine = StartCoroutine(ExecuteAfterTimeCoroutine(seconds, action));

            return coroutine;
        }

        protected IEnumerator ExecuteAfterTimeCoroutine(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);

            action();
        }

        #endregion

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