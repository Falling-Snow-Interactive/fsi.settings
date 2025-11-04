using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using Button = UnityEngine.UIElements.Button;

namespace Fsi.Settings.Builder
{
    public class SettingsBuilderEditor : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset treeAsset = null;
        
        // References
        private Label locationPath;
        private Button locationButton;
        private TextField nameField;

        [MenuItem("Assets/Create/Fsi/Project Settings")]
        public static void OpenWindow()
        {
            SettingsBuilderEditor wnd = GetWindow<SettingsBuilderEditor>();
            wnd.titleContent = new GUIContent("Settings Builder");
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            treeAsset.CloneTree(root);

            locationPath = root.Q<Label>("location_path");
            locationButton = root.Q<Button>("location_button");
            nameField = root.Q<TextField>("name_field");
            
            locationPath.text = AssetDatabase.GetAssetPath(Selection.activeObject);

            locationButton.clicked += () =>
                                      {
                                          string path = EditorUtility.OpenFolderPanel("Choose Location", "", "");
                                          locationPath.text = path;
                                      };

            Button confirmButton = root.Q<Button>("confirm_button");
            confirmButton.clicked += () =>
                                     {
                                         string path = locationPath.text;
                                         string name = nameField.value;

                                         CreateSettings(path, name);
                                         CreateSettingsProvider(path, name);
                                         Close();
                                     };
        }

        private void CreateSettings(string path, string name)
        {
            if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(name))
            {
                EditorUtility.DisplayDialog("Invalid Input", "Please choose a location and enter a name.", "OK");
                return;
            }

            // Resolve the script folder (where the template lives)
            string scriptPath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
            string scriptDir = Path.GetDirectoryName(scriptPath);

            // Load template as TextAsset via AssetDatabase
            string templateAssetPath = Path.Combine(scriptDir, "settings_template.txt").Replace("\\", "/");
            TextAsset template = AssetDatabase.LoadAssetAtPath<TextAsset>(templateAssetPath);
            if (template == null)
            {
                EditorUtility.DisplayDialog("Template Missing", $"Could not find settings_template.txt at:\n{templateAssetPath}", "OK");
                return;
            }

            // Replace placeholder
            string output = template.text.Replace("[NAME]", name);

            // Ensure target directory exists
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            // Write new file
            string fileName = $"{name}Settings.cs";
            string absoluteOutPath = Path.Combine(path, fileName);
            File.WriteAllText(absoluteOutPath, output);

            // If saved under Assets/, refresh so Unity imports it
            string projectAssets = Application.dataPath.Replace("\\", "/");
            if (absoluteOutPath.Replace("\\", "/").StartsWith(projectAssets))
            {
                AssetDatabase.Refresh();
            }

            Debug.Log($"Created settings script at: {absoluteOutPath}");
        }

        private void CreateSettingsProvider(string path, string name)
        {
            if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(name))
            {
                EditorUtility.DisplayDialog("Invalid Input", "Please choose a location and enter a name.", "OK");
                return;
            }

            // Resolve the script folder (where the provider template lives)
            string scriptPath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
            string scriptDir = Path.GetDirectoryName(scriptPath);

            // Load provider template as TextAsset via AssetDatabase
            string providerTemplateAssetPath = Path.Combine(scriptDir, "settingsprovider_template.txt").Replace("\\", "/");
            TextAsset providerTemplate = AssetDatabase.LoadAssetAtPath<TextAsset>(providerTemplateAssetPath);
            if (providerTemplate == null)
            {
                EditorUtility.DisplayDialog("Template Missing", $"Could not find settingsprovider_template.txt at:\n{providerTemplateAssetPath}", "OK");
                return;
            }

            // Replace placeholder
            string providerOutput = providerTemplate.text
                .Replace("[NAME]", name)
                .Replace("[PROJECT]", Application.productName);

            // Ensure Editor directory exists under the chosen path
            string editorDir = Path.Combine(path, "Editor");
            if (!Directory.Exists(editorDir))
                Directory.CreateDirectory(editorDir);

            // Write Provider file
            string providerFileName = $"{name}SettingsProvider.cs";
            string providerAbsoluteOutPath = Path.Combine(editorDir, providerFileName);
            File.WriteAllText(providerAbsoluteOutPath, providerOutput);

            // If saved under Assets/, refresh so Unity imports it
            string projectAssets = Application.dataPath.Replace("\\", "/");
            if (providerAbsoluteOutPath.Replace("\\", "/").StartsWith(projectAssets))
            {
                AssetDatabase.Refresh();
            }

            Debug.Log($"Created settings provider script at: {providerAbsoluteOutPath}");
        }
    }
}
