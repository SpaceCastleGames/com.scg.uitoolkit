using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
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

            if ("string".Equals(property.type) && string.IsNullOrWhiteSpace(property.stringValue))
            {
                AddRequiredLabel(ve);
            }
            else if (property.type.StartsWith("PPtr") && property.objectReferenceValue == null)
            {
                AddRequiredLabel(ve);
            }

            var propertyField = new PropertyField(property);
            ve.Add(propertyField);
            return ve;
        }

        public void AddRequiredLabel(VisualElement ve)
        {
            Label label = new Label("Value is required for this property!");
            label.style.color = Color.red;
            ve.Add(label);
        }


        //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        //{
        //    EditorGUI.LabelField(position, label.text, "Use with strings only.");
        //}


    }
}