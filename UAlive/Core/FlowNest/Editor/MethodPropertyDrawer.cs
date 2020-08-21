using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Ludiq;
using Bolt;

namespace Lasm.UAlive
{
    [CustomPropertyDrawer(typeof(Method))]
    public class MethodPropertyDrawer : PropertyDrawer
    {
        #region Variables
        Texture2D icon;
        SerializedProperty source, macro, embed, hidden, prop;
        Method nest;
        GUIStyle iconStyle;
        Rect backgroundRect, labelRect, iconRect, editRect;
        #endregion

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 24;
        }

        private void Init(SerializedProperty property)
        {
            if (macro == null) macro = property.FindPropertyRelative("macro");

            prop = property;
            nest = (Method)fieldInfo.GetValue(prop.serializedObject.targetObject);

            if (icon == null)
            {
                var images = AssetDatabase.FindAssets("Bolt.FlowMacro");
                var path = AssetDatabase.GUIDToAssetPath(images[0]);
                icon = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
            }

            iconStyle = new GUIStyle();
            iconStyle.normal.background = icon;
        }

        private void CreateRects(Rect position, SerializedProperty property, GUIContent label)
        {
            backgroundRect = position;
            backgroundRect.height = GetPropertyHeight(property, label) - 2;

            labelRect = position;
            labelRect.height = 16;
            labelRect.x += 24;
            labelRect.y += 2;

            iconRect = backgroundRect;
            iconRect.y += 3;
            iconRect.x += 6;
            iconRect.width = 16;
            iconRect.height = 16;

            editRect = iconRect;
            editRect.x += 120;
            editRect.width = 60;
            editRect.height = 16;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Init(property);

            property.serializedObject.Update();

            CreateRects(position, property, label);

            GUI.Label(labelRect, property.displayName);

            GUI.Box(backgroundRect, GUIContent.none, new GUIStyle(EditorStyles.helpBox));

            if (icon != null) EditorGUI.LabelField(iconRect, GUIContent.none, iconStyle);

            EditButton(editRect);

            property.serializedObject.ApplyModifiedProperties();
        }

        private void EditButton(Rect position)
        {
            if (GUI.Button(position, "Edit"))
            {
                GraphWindow.OpenActive(GraphReference.New((MethodMacro)macro.objectReferenceValue, true));
            }
        }

        private void DebugCodeButton(Rect position)
        {

        }

        private void CopyCodeButton(Rect position)
        {

        }
    }
}