using fsi.settings.Globals;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Settings.Globals
{
	[CustomPropertyDrawer(typeof(GlobalVariable<>))]
	public abstract class GlobalVariableDrawer<TVal, TField> : PropertyDrawer where TField : BaseField<TVal>
	{
		protected virtual string DocumentPath => "Packages/com.fallingsnowinteractive.settings/Editor/Globals/GlobalVariableEntryDoc.uxml";

        private const string NameTextId = "name_text";
        private const string OverrideToggleId = "override_toggle";
        private const string OverrideFieldId = "override_field";

        private const string UseOverridePropName = "useOverride";
        private const string OverridePropName = "overrideValue";

        private Toggle useOverrideToggle;
        private TField overrideField;
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new();

            VisualTreeAsset entryDoc = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(DocumentPath);
            entryDoc.CloneTree(root);

            Label nameText = root.Q<Label>(NameTextId);
            nameText.text = property.displayName;

	        useOverrideToggle = root.Q<Toggle>(OverrideToggleId);
            SerializedProperty useOverrideProp = property.FindPropertyRelative(UseOverridePropName);
            useOverrideToggle.BindProperty(useOverrideProp);

	        overrideField = root.Q<TField>(OverrideFieldId);
            SerializedProperty overrideProp = property.FindPropertyRelative(OverridePropName);
            overrideField.label = "";
            overrideField.BindProperty(overrideProp);

            useOverrideToggle.RegisterValueChangedCallback(evt =>
                                                      {
                                                          overrideField.enabledSelf = evt.newValue;
                                                      });
            overrideField.enabledSelf = useOverrideToggle.value;

            return root;
        }
	}
}