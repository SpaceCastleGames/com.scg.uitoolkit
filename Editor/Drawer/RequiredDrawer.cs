using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using scg.uitoolkit.runtime;
namespace scg.uitoolkit.editor
{
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public class RequiredDrawer : PropertyDrawer
    {


        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var ve = new VisualElement();
            UpdateView(property, ve);
            return ve;
        }

        private void UpdateView(SerializedProperty property, VisualElement ve)
        {
            Label label = new Label("Value is required for this property!");
            label.style.color = Color.red;
            ve.Add(label);
            UpdateLabelState(property, label);
            var propertyField = new PropertyField(property);
            propertyField?.RegisterValueChangeCallback(value =>
            {
                UpdateLabelState(property, label);
            });
            ve.Add(propertyField);
        }

        public void UpdateLabelState(SerializedProperty property, Label label)
        {
            if ("string".Equals(property.type) && string.IsNullOrWhiteSpace(property.stringValue))
            {
                label.style.display = DisplayStyle.Flex;
            }
            else if (property.type.StartsWith("PPtr") && property.objectReferenceValue == null)
            {
                label.style.display = DisplayStyle.Flex;
            }
            else
            { 
                label.style.display = DisplayStyle.None;
            }
        }

    }
}