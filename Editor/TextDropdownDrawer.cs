using scg.uitoolkit.runtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UIElements;

namespace scg.uitoolkit.editor
{
    [CustomPropertyDrawer(typeof(TextDropdownAttribute))]
    public class TextDropdownDrawer : PropertyDrawer
    {

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var atrib = attribute as TextDropdownAttribute;

            var ve = new VisualElement();

            SerializedObject serializedObject = property.serializedObject;
            System.Reflection.MethodInfo info =
            serializedObject.targetObject.GetType().GetMethod(atrib.MethodName,
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance| System.Reflection.BindingFlags.Public);

            
            if (info == null)
            {
                Label label = new Label("Could not find method.");
                label.style.color= Color.red;
                ve.Add(label);
            }
            else
            {
                var codeList = (List<string>)info.Invoke(serializedObject.targetObject, null);
                DropdownField dropdownField = new DropdownField(codeList, 0);

                //Select starting value.
                int startingIndex = codeList.IndexOf(property.stringValue);
                startingIndex=startingIndex<0?0:startingIndex;
                dropdownField.index= startingIndex;

                dropdownField.RegisterValueChangedCallback(v =>  Debug.Log($"Dropdownchanged to: {v.newValue}"));
                dropdownField.RegisterValueChangedCallback(v => {
                    Debug.Log($"Dropdownchanged to: {v.newValue}");
                    property.stringValue = v.newValue;
                    property.serializedObject.ApplyModifiedProperties();
                });
                //property.serializedObject.ApplyModifiedProperties();

                ve.Add(dropdownField);
            }

            return ve;
        }



        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // First get the attribute since it contains the range for the slider
            TextDropdownAttribute range = attribute as TextDropdownAttribute;

            // Now draw the property as a Slider or an IntSlider based on whether it's a float or integer.
            if (property.propertyType == SerializedPropertyType.String)
                EditorGUI.TextField(position, property.stringValue);

            else
                EditorGUI.LabelField(position, label.text, "Use with strings only.");
        }


    }
}