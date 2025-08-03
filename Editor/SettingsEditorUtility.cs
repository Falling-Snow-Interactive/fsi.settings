// using Fsi.Ui.Labels;

using Fsi.Ui.Labels;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Settings
{
	public static class SettingsEditorUtility
	{
		/*
		 * SerializedProperty locEffectProp = settingsProp.FindProperty(nameof(CardSettings.locCardEffect));
		   PropertyField locEffectField = new(locEffectProp);
		   scrollView.Add(locEffectField);
		 */
		
		public static VisualElement CreateSettingsProperty(string name, SerializedObject serializedObject)
		{
			SerializedProperty property = serializedObject.FindProperty(name);
			PropertyField field = new(property);
			return field;
		}

		public static VisualElement CreateSection(SerializedObject serializedObject, string title, string[] propertyNames)
		{
			var section = new Box
			              {
				              style =
				              {
					              marginTop = 0,
					              marginLeft = 10,
					              marginRight = 10,
					              marginBottom = 0
				              }
			              };

			var label = LabelUtility.Section(title);
			section.Add(label);

			foreach (string propName in propertyNames)
			{
				var prop = new PropertyField(serializedObject.FindProperty(propName));
				prop.Bind(serializedObject);
				section.Add(prop);
			}

			return section;
		}
		
		public static VisualElement CreateIMGUISection(SerializedObject serializedObject, string title, string[] propertyNames)
		{
			var section = new VisualElement
			              {
				              style =
				              {
					              marginTop = 10,
					              marginBottom = 10
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