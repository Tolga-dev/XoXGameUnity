using System.Collections.Generic;
using UnityEngine;

namespace SoScripts
{
    [CreateAssetMenu(fileName = "PopupConfig", menuName = "Configurations/PopupConfig")]
    public class PopupConfig : ScriptableObject
    {
        public List<Popup> popups;
    }
}
