using System.Collections.Generic;
using UnityEngine;

namespace So
{
    [CreateAssetMenu(fileName = "FxData", menuName = "So/FxData", order = 0)]
    public class FxData : ScriptableObject
    {
        public List<GameObject> fxList = new List<GameObject>();
        
        public GameObject GetRandomFx()
        {
            return fxList[Random.Range(0, fxList.Count)];
        }
    }
}