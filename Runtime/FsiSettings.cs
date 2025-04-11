using UnityEditor;
using UnityEngine;

namespace fsi.settings
{
    public class FsiSettings : ScriptableObject
    {
        // private const string RESOURCE_PATH = "Fsi/Settings";
        // private const string FULL_PATH = "Assets/Resources/" + RESOURCE_PATH + ".asset";
        //
        // #region Default Settings Methods
        //
        // public static FsiSettings GetOrCreateSettings()
        // {
        //     var settings = Resources.Load<FsiSettings>(RESOURCE_PATH);
        //
        //     #if UNITY_EDITOR
        //     if (!settings)
        //     {
        //         if (!AssetDatabase.IsValidFolder("Assets/Resources"))
        //         {
        //             AssetDatabase.CreateFolder("Assets", "Resources");
        //         }
        //
        //         if (!AssetDatabase.IsValidFolder("Assets/Resources/Settings"))
        //         {
        //             AssetDatabase.CreateFolder("Assets/Resources", "Settings");
        //         }
        //
        //         settings = CreateInstance<FsiSettings>();
        //         AssetDatabase.CreateAsset(settings, FULL_PATH);
        //         AssetDatabase.SaveAssets();
        //     }
        //     #endif
        //
        //     return settings;
        // }
        //
        // #if UNITY_EDITOR
        //
        // public static SerializedObject GetSerializedSettings()
        // {
        //     return new SerializedObject(GetOrCreateSettings());
        // }
        // #endif
        //
        // #endregion
    }
}