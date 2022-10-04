using UnityEngine;

namespace YellowJelloGames.YToolbox.YSceneManagement.Runtime
{
    [CreateAssetMenu(menuName = "Scene Data/Default")]
    public class YSceneAsset : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField, TextArea(5, 10)]
        private string developerDescription;
#endif
        [SerializeField]
        private YSceneAssetReference assetReference;

        [SerializeField]
        private YSceneType type;

        public YSceneAssetReference AssetReference => assetReference;
        public YSceneType Type => type;

        public string DisplayName
        {
            get
            {
#if UNITY_EDITOR
                return UnityEditor.ObjectNames.NicifyVariableName(name.Replace("_", " "));
#else
                return name;
#endif
            }
        }
    }
}