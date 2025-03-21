using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeZijlen.SceneSelector
{
    [CreateAssetMenu(fileName = "ButtonInfoScriptableObject", menuName = "Scriptable Objects/ButtonInfoScriptableObject")]
    public class ButtonInfoScriptableObject : ScriptableObject
    {
        public SceneAsset scene;
    }
}
