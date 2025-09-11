using Fsi.Ui.Labels;
using Fsi.Ui.Spacers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Settings
{
	public static class SettingsEditorUtility
	{
		public static VisualElement CreateSettingsProperty(string name, SerializedObject serializedObject)
		{
			SerializedProperty property = serializedObject.FindProperty(name);
			PropertyField field = new(property);
			return field;
		}

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
	}
}