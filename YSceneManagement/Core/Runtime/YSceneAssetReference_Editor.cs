#if UNITY_EDITOR
using UnityEditor;

namespace YellowJelloGames.YToolbox.YSceneManagement.Runtime
{
    public partial class YSceneAssetReference
    {
        public SceneAsset Scene { get; private set; }
        
        public YSceneAssetReference(SceneAsset scene)
        {
            Scene = scene;
        }

        public override bool ValidateAsset(string path)
        {
            return ValidateAsset(AssetDatabase.LoadAssetAtPath<SceneAsset>(path));
        }

        public override bool ValidateAsset(UnityEngine.Object obj)
        {
            return obj is SceneAsset;
        }

        public override bool SetEditorAsset(UnityEngine.Object value)
        {
            if (!base.SetEditorAsset(value)) return false;

            if (value is SceneAsset scene)
            {
                Scene = scene;
                return true;
            }

            Scene = null;
            return false;
        }
    }
}
#endif