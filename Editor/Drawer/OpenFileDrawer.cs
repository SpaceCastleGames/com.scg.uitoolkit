using scg.uitoolkit.runtime;
using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace scg.uitoolkit.editor
{
    [CustomPropertyDrawer(typeof(OpenFileAttribute))]
    public class OpenFileDrawer : PropertyDrawer
    {

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var ve = new VisualElement();
            //property.serializedObject.Update();
            if (!"string".Equals(property.type) && string.IsNullOrWhiteSpace(property.stringValue))
            {
                Label label = new Label("Attribute needs to be used on string!");
                label.style.color = Color.red;
                ve.Add(label);
            }
            ve.style.flexDirection = FlexDirection.Row;
            var propertyField = new PropertyField(property);
            propertyField.style.flexGrow = 1;
            ve.Add(propertyField);
            ve.Add(AddButton(property));

            return ve;
        }

        private Button AddButton(SerializedProperty property)
        {
            Button openFileBtn = new Button();
            openFileBtn.style.marginLeft = 5;
            Image folderImg = new Image();

            folderImg.image = AssetDatabase.LoadAssetAtPath<Texture>("Packages/com.scg.uitoolkit/Editor/images/open-folder.png");

            openFileBtn.Add(folderImg);
            openFileBtn.clicked += () =>
            {
                var pathName = "";
                if (!string.IsNullOrWhiteSpace(property.stringValue))
                {
                    pathName = Path.GetDirectoryName(property.stringValue);
                }

                var atrib = attribute as OpenFileAttribute;
                var path = "";
                if (atrib.SelectDirectory)
                {
                    path = EditorUtility.OpenFolderPanel("Select Directory", pathName, "");
                }
                else
                {
                    path = EditorUtility.OpenFilePanel("Select File", pathName, "");
                }
                
                if (atrib.RelativeToAssetDirectory && path.StartsWith(Application.dataPath))
                {
                    path = "Assets" + path.Substring(Application.dataPath.Length);
                }

                if (!string.IsNullOrWhiteSpace(path))
                {
                    property.stringValue = path;
                }

                property.serializedObject.ApplyModifiedProperties();
                
            };
            return openFileBtn;
        }

    }
}