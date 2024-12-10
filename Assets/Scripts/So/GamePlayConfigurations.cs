using UnityEngine;

namespace So
{
    [CreateAssetMenu(fileName = "GamePlayConfigurations", menuName = "So/GamePlayConfigurations", order = 0)]
    public class GamePlayConfigurations : ScriptableObject
    {
        public bool gameEffectSound;

        public AudioClip GetSound(string newClipName)
        {
            return null;
        }
    }
}