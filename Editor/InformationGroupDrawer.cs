using fsi.settings.Informations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Settings
{
	[CustomPropertyDrawer(typeof(InformationGroup<,>), true)]
	public class InformationGroupDrawer : PropertyDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			VisualElement root = new();

			SerializedProperty informationProp = property.FindPropertyRelative("information");
			PropertyField informationField = new(informationProp){label = property.displayName };
			root.Add(informationField);

			return root;
		}
	}
}