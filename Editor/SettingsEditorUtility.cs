using System;
using Fsi.Ui.Labels;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Spacer = Fsi.Ui.Dividers.Spacer;

namespace Fsi.Settings
{
	public static class SettingsEditorUtility
	{
		private const string StylesheetPath = "Packages/com.fallingsnowinteractive.ui/Assets/FsiUi.uss";
		private const float SettingsMargin = 5f;

		public static SettingsProvider CreateSettingsProvider(string label, string path, Action<string, VisualElement> onActivate)
		{
			SettingsProvider provider = new(path, SettingsScope.Project)
			                            {
				                            label = label,
				                            activateHandler = onActivate,
			                            };
        
			return provider;
		}

		public static SettingsProvider CreateSettingsProvider(string label, string path, SerializedObject prop)
		{
			SettingsProvider provider = new(path, SettingsScope.Project)
			                            {
				                            label = label,
				                            activateHandler = (s, element) =>
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
			
			// scroll.Add(new Spacer());
			// Can maybe put a toolbar here...
			
			scroll.Add(new Spacer());
			scroll.Add(new InspectorElement(prop));
			scroll.Add(new Spacer());
			
			scroll.Bind(prop);

			return scroll;
		}
		
		#region Obsolete
		
		[Obsolete]
		public static VisualElement CreateSettingsProperty(string name, SerializedObject serializedObject)
		{
			SerializedProperty property = serializedObject.FindProperty(name);
			PropertyField field = new(property);
			return field;
		}
		
		[Obsolete]
		public static VisualElement CreateTitle(string title, string description)
		{
			VisualElement titleSection = new() { style = { flexGrow = 0, flexShrink = 0 } };
			Label titleLabel = LabelUtility.Title(title);
			Label descriptionLabel = new Label(description);
			
			titleSection.Add(titleLabel);
			titleSection.Add(descriptionLabel);
			titleSection.Add(new Spacer());

			return titleSection;
		}

		[Obsolete]
		public static VisualElement CreateSection(string name, VisualElement[] categories)
		{
			var foldout = new Foldout() { text = name, value = EditorPrefs.GetBool($"Section.{name}", false) };
			foldout.RegisterValueChangedCallback(evt =>
			                                     {
				                                     EditorPrefs.SetBool($"Section.{name}", foldout.value);
			                                     });

			foreach (var cat in categories)
			{
				foldout.Add(cat);
			}

			return foldout;
		}
		
		[Obsolete]
		public static VisualElement CreateCategory(SerializedObject serializedObject, string name, string[] properties)
		{
			var foldout = new Foldout() { text = name, value = EditorPrefs.GetBool($"Category.{name}", false) };
			foldout.RegisterValueChangedCallback(evt =>
			                                     {
				                                     EditorPrefs.SetBool($"Category.{name}", evt.newValue);
			                                     });

			foreach (string p in properties)
			{
				var prop = new PropertyField(serializedObject.FindProperty(p));
				prop.Bind(serializedObject);
				foldout.Add(prop);
			}

			return foldout;
		}
		
		[Obsolete]
		public static VisualElement CreateIMGUISection(SerializedObject serializedObject, string title, string[] propertyNames)
		{
			var section = new Box
			              {
				              style =
				              {
					              paddingTop = 10,
					              paddingBottom = 10,
					              paddingLeft = 10,
					              paddingRight = 10,
					              
					              marginTop = 5,
					              marginBottom = 5,
					              marginLeft = 5,
					              marginRight = 5,
				              }
			              };

			var label = LabelUtility.Section(title);
			section.Add(label);

			var imguiContainer = new IMGUIContainer(() =>
			                                        {
				                                        serializedObject.Update();

				                                        foreach (string propName in propertyNames)
				                                        {
					                                        EditorGUILayout.PropertyField(serializedObject.FindProperty(propName));
					                                        serializedObject.ApplyModifiedProperties();
				                                        }
			                                        });

			section.Add(imguiContainer);

			return section;
		}
		
		#endregion
	}
}