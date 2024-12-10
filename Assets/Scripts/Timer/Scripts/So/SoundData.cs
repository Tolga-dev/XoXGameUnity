using System;
using System.Collections.Generic;
using UnityEngine;

namespace So
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "Data/SoundData", order = 0)]
    public class SoundData : ScriptableObject
    {
        public AudioClip timeClip;

    }

}