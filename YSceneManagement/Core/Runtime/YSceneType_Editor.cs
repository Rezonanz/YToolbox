#if UNITY_EDITOR
using UnityEngine;

namespace YellowJelloGames.YToolbox.YSceneManagement.Runtime
{
    public partial class YSceneType
    {
        [Header("Editor")]
        [SerializeField]
        private Color editorColor;

        public Color EditorColor => editorColor;
    }
}
#endif