using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using YellowJelloGames.YToolbox.YSceneManagement.Runtime;

namespace YellowJelloGames.YToolbox.YSceneManagement.Editor
{
    public class SceneSelector : EditorWindow, IHasCustomMenu
    {
        [MenuItem("Tools/Scene Selector")]
        private static void Open()
        {
            GetWindow<SceneSelector>("Scene Selector");
        }

        private readonly List<YSceneAsset> _scenes = new();

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }


        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange newMode)
        {
            switch (newMode)
            {
                case PlayModeStateChange.EnteredEditMode:
                    rootVisualElement.SetEnabled(true);
                    break;
                
                case PlayModeStateChange.EnteredPlayMode:
                    rootVisualElement.SetEnabled(false);
                    break;
            }
        }

        public void AddItemsToMenu(GenericMenu menu)
        {
            menu.AddItem(new GUIContent("Refresh"), false, Refresh);
        }

        private void CreateGUI()
        {
            CreateScenesList();
        }

        private void Refresh()
        {
            CreateScenesList();
        }

        private void CreateScenesList()
        {
            rootVisualElement.Clear();
            _scenes.Clear();

            string[] guids = AssetDatabase.FindAssets($"t:{typeof(YSceneAsset)}");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<YSceneAsset>(path);
                if (asset != null)
                {
                    _scenes.Add(asset);
                }
            }

            var list = new ListView
            {
                style =
                {
                    flexGrow = 1
                },
                itemsSource = _scenes,
                fixedItemHeight = 20,
                showAlternatingRowBackgrounds = AlternatingRowBackground.All,
                makeItem = () =>
                {
                    var element = new VisualElement
                    {
                        style =
                        {
                            alignItems = Align.Center,
                            flexDirection = FlexDirection.Row,
                            paddingLeft = 6,
                            paddingRight = 6,
                        }
                    };

                    var icon = new VisualElement
                    {
                        name = "icon",
                        style =
                        {
                            width = 4,
                            height = 4,

                            borderBottomLeftRadius = Length.Percent(50),
                            borderTopLeftRadius = Length.Percent(50),
                            borderBottomRightRadius = Length.Percent(50),
                            borderTopRightRadius = Length.Percent(50),

                            marginRight = 4,
                        }
                    };

                    var label = new Label
                    {
                        name = "label",
                        style =
                        {
                            flexGrow = 1,
                        },
                    };

                    var typeLabel = new Label
                    {
                        name = "type",
                        style =
                        {
                            position = Position.Absolute,
                            right = 0,
                            opacity = 0.5f,
                            fontSize = 9,
                        }
                    };

                    element.Add(icon);
                    element.Add(label);
                    element.Add(typeLabel);

                    return element;
                },
                bindItem = (element, i) =>
                {
                    var sceneAsset = _scenes[i];

                    var label = element.Q<Label>("label");
                    label.text = sceneAsset.DisplayName;

                    if (sceneAsset.Type)
                    {
                        var icon = element.Q("icon");
                        icon.style.backgroundColor = sceneAsset.Type.EditorColor;
                    }

                    string scenePath = AssetDatabase.GetAssetPath(sceneAsset.AssetReference.editorAsset);
                    element.SetEnabled(!string.IsNullOrEmpty(scenePath));
                    element.AddManipulator(new ContextualMenuManipulator(evt =>
                    {
                        var dropdownMenu = evt.menu;
                        dropdownMenu.AppendAction("Open", _ => { OnSceneButtonClick(i); });
                        dropdownMenu.AppendSeparator();
                        dropdownMenu.AppendAction("Select Asset", _ =>
                        {
                            EditorGUIUtility.PingObject(sceneAsset);
                            Selection.activeObject = sceneAsset;
                        });
                        dropdownMenu.AppendAction("Select Scene", _ =>
                        {
                            EditorGUIUtility.PingObject(sceneAsset.AssetReference.editorAsset);
                            Selection.activeObject = sceneAsset.AssetReference.editorAsset;
                        });
                    }));

                    if (sceneAsset.Type)
                    {
                        var typeLabel = element.Q<Label>("type");
                        typeLabel.text = sceneAsset.Type.DisplayName;
                    }
                },
                selectionType = SelectionType.Single,
            };

            list.selectedIndicesChanged += ints => OnSceneButtonClick(ints.First());

            list.StretchToParentSize();

            rootVisualElement.Add(list);
        }

        private void OnSceneButtonClick(int i)
        {
            if (Application.isPlaying)
            {
                Debug.LogWarning("Cannot switch scene during play mode.");
                return;
            }

            string scenePath = AssetDatabase.GetAssetPath(_scenes[i].AssetReference.editorAsset);
            if (string.IsNullOrEmpty(scenePath)) return;
            
            var activeScene = SceneManager.GetActiveScene();
            if (activeScene.path == scenePath)
            {
                Debug.LogWarning("Scene is open.");
                return;
            }

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(scenePath);
            }
        }
    }
}