using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace fsi.settings.Editor.Builder
{
    public class SettingsBuilderEditor : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset treeAsset = null;

        [MenuItem("Window/UI Toolkit/SettingsBuilderEditor")]
        public static void OpenWindow()
        {
            SettingsBuilderEditor wnd = GetWindow<SettingsBuilderEditor>();
            wnd.titleContent = new GUIContent("Settings Builder");
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            treeAsset.CloneTree(root);
        }
    }
}
