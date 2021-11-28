namespace HairyNerd.CuteSandwich.Unity.Behaviours
{
    using UnityEngine;

    [AddComponentMenu("HairyNerd/CuteSandwich/MusicBehaviour")]

    public class MusicBehaviour : BehaviourBase
    {
        public AudioSource MusicSource;

        public void Stop()
        {
            MusicSource.Stop();
        }
    }
}