using Fsi.Ui.Labels;
using Fsi.Ui.Spacers;
using UnityEditor;
using UnityEditor.UIElements;
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

		public static VisualElement CreateTitle(string title, string subtitle)
		{
			VisualElement titleSection = new() { style = { flexGrow = 0, flexShrink = 0 } };
			Label titleLabel = LabelUtility.Title(title);
			Label subtitleLabel = new Label(subtitle);
			
			titleSection.Add(titleLabel);
			titleSection.Add(subtitleLabel);
			titleSection.Add(new Spacer());

			return titleSection;
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
		
		public static VisualElement CreateSection(SerializedObject serializedObject, string title, (string,string)[] propertyNames)
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

			foreach ((string,string) p in propertyNames)
			{
				var prop = new PropertyField(serializedObject.FindProperty(p.Item1))
				           {
					           label = p.Item2
				           };
				prop.Bind(serializedObject);
				section.Add(prop);
			}

			return section;
		}
		
		public static VisualElement CreateCategory(SerializedObject serializedObject, string category, string[] propertyNames)
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

			var label = LabelUtility.Category(category);
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