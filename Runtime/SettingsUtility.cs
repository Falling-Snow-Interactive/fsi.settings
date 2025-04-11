#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace fsi.settings
{
    public static class SettingsUtility
    {
        public static void GenerateEnum(string enumName, string filePathAndName, string ns, List<string> keys)
        {
            using (var streamWriter = new StreamWriter(filePathAndName))
            {
                streamWriter.WriteLine($"namespace {ns}");
                streamWriter.WriteLine("{");
                streamWriter.WriteLine( "\tpublic enum " + enumName );
                streamWriter.WriteLine( "\t{" );
                streamWriter.WriteLine( "\t\tNone = 0,");
                for (int i = 0; i < keys.Count; i++)
                {
                    string key = keys[i];
                    streamWriter.WriteLine($"\t\t{key} = {i + 1},");
                }
                streamWriter.WriteLine( "\t}" );
                streamWriter.WriteLine("}");
            }
            AssetDatabase.Refresh();
            Debug.Log($"{enumName} enum generated at {filePathAndName} containing {keys.Count} types");
        }
    }
}
#endif