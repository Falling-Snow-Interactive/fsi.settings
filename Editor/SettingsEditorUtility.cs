using System;
using System.Linq;
using System.Reflection;
using Fsi.Ui.Dividers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Settings
{
	public static class SettingsEditorUtility
	{
		private const string StylesheetPath = "Packages/com.fallingsnowinteractive.ui/Assets/FsiUi.uss";
		private const float SettingsMargin = 5f;

		public static SettingsProvider CreateSettingsProvider<T>(string label, string path, SerializedObject prop)
		{
			if (!HasSettings(typeof(T)))
			{
				return default;
			}
			
			SettingsProvider provider = new(path, SettingsScope.Project)
			                            {
				                            label = label,
				                            activateHandler = (_, element) =>
				                                               {
					                                               element.Add(CreateSettingsPage(prop, label));
				                                               },
			                            };
        
			return provider;
		}
		
		public static VisualElement CreateSettingsPage(SerializedObject prop, string name)
		{
			ScrollView scroll = new()
			                    {
				                    style =
				                    {
					                    marginTop = SettingsMargin,
					                    marginRight = SettingsMargin,
					                    marginBottom = SettingsMargin,
					                    marginLeft = SettingsMargin,
				                    },
			                    };
			StyleSheet uss = AssetDatabase.LoadAssetAtPath<StyleSheet>(StylesheetPath);
			if (uss)
			{
				scroll.styleSheets.Add(uss);
			}

			Label title = new($"{name} Settings");
			title.AddToClassList("title");
			scroll.Add(title);
			
			scroll.Add(new Divider());
			scroll.Add(new InspectorElement(prop));
			
			scroll.Bind(prop);

			return scroll;
		}

		public static bool HasSettings(Type type)
		{
			const BindingFlags flags = BindingFlags.Instance 
			                           | BindingFlags.Public 
			                           | BindingFlags.NonPublic;

			return type
			       .GetFields(flags)
			       .Any(field =>
				            !field.IsStatic 
				            && !field.IsDefined(typeof(NonSerializedAttribute)) 
				            && (
					               field.IsPublic 
					               || field.IsDefined(typeof(SerializeField))
					               )
			           );
		}

		public static void SetUss(VisualElement root)
		{
			StyleSheet uss = AssetDatabase.LoadAssetAtPath<StyleSheet>(StylesheetPath);
			if (uss)
			{
				root.styleSheets.Add(uss);
			}
		}
	}
}