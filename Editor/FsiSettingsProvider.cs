using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace fsi.settings.Editor
{
    public class FsiSettingsProvider : SettingsProvider
    {
        // private const string SETTINGS_PATH = "Fsi/Settings";
        // private SerializedObject serializedSettings;
        //
        // private FsiSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) 
        //     : base(path, scopes, keywords)
        // {
        // }
        //
        // [SettingsProvider]
        // public static SettingsProvider CreateMyCustomSettingsProvider()
        // {
        //     return new FsiSettingsProvider<T>(SETTINGS_PATH, SettingsScope.Project);
        // }
        //
        // public override void OnActivate(string searchContext, VisualElement rootElement)
        // {
        //     // TODO - Need to make this go to T, not sure if possible.
        //     serializedSettings = FsiSettings.GetSerializedSettings();
        // }
        //
        // public override void OnGUI(string searchContext)
        // {
        //     // EditorGUILayout.PropertyField(serializedSettings.FindProperty("property"));
        //     
        //     EditorGUILayout.Space(20);
        //     if (GUILayout.Button("Save"))
        //     {
        //         serializedSettings.ApplyModifiedProperties();
        //     }
        //     
        //     serializedSettings.ApplyModifiedProperties();
        // }
        public FsiSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords)
        {
        }
    }
}