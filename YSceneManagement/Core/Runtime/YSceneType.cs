using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace YellowJelloGames.YToolbox.YSceneManagement.Runtime
{
    [CreateAssetMenu(menuName = "Scene Data/Type")]
    public partial class YSceneType : ScriptableObject
    {
        [SerializeField]
        private List<YSceneAsset> dependencies;

        public ReadOnlyCollection<YSceneAsset> Dependencies => dependencies.AsReadOnly();

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