using System.Collections.Generic;
using UnityEngine;

namespace DeZijlen.SceneSelector
{
    [CreateAssetMenu(fileName = "LevelContainer", menuName = "Scriptable Objects/LevelContainer")]
    public class LevelContainer : ScriptableObject
    {
        public List<ButtonInfoScriptableObject> buttonInfo;
    }
}
