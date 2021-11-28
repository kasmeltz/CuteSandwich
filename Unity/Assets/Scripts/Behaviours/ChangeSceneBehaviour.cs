namespace HairyNerd.CuteSandwich.Unity.Behaviours
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    [AddComponentMenu("HairyNerd/CuteSandwich/ChangeScene")]

    public class ChangeSceneBehaviour : BehaviourBase
    {
        public void ChangeScene(string scene)
        {
            SceneManager.LoadSceneAsync(scene);
        }
    }
}