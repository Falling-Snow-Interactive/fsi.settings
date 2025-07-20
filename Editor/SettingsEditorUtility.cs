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
	}
}