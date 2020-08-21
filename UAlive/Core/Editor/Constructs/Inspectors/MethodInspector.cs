using UnityEngine;
using UnityEditor;
using Ludiq;

namespace Lasm.UAlive
{
    [Inspector(typeof(Method))]
    public sealed class MethodInspector : Inspector
    {
        #region Variables
        Texture2D icon;
        Metadata source, macro, prop;
        Method nest;
        Rect backgroundRect, labelRect, typeRect, iconRect, editRect, toggleRect;
        #endregion

        public MethodInspector(Metadata metadata) : base(metadata)
        {
        }

        protected override float GetHeight(float width, GUIContent label)
        {
            return 26;
        }

        private void Init()
        {
            if (macro == null) macro = metadata["macro"];

            prop = metadata;
            nest = (Method)metadata.value;
        }

        private void CreateRects(Rect position, GUIContent label)
        {
            backgroundRect = position;
            backgroundRect.height = GetHeight(position.width, label) - 2;

            if (nest != null && nest.isSpecial) {
                iconRect = backgroundRect;
                iconRect.y += 4;
                iconRect.x += 5;
                iconRect.width = 16;
                iconRect.height = 16;
            }

            typeRect = nest != null && nest.isSpecial ? backgroundRect.Add().X(iconRect.width + 4).Set().Width(80).Add().Y(3).Add().X(4) : backgroundRect.Set().Width(80).Add().Y(3).Add().X(4);
            typeRect.height = 16;

            if (string.IsNullOrEmpty(nest.name) || string.IsNullOrWhiteSpace(nest.name) || !nest.showLabel)
            {
                typeRect.width = position.width - 44 - 12;
            }

            if (nest.hasOptionalOverride)
            {
                toggleRect = typeRect;
                toggleRect.width = 16;
                toggleRect.x += 82;
                toggleRect.y += 1;
            }

            labelRect = position;
            labelRect.height = 16;
            labelRect.width = GUI.skin.label.CalcSize(new GUIContent(label)).x + 6;
            labelRect.x = typeRect.x + typeRect.width + 18;
            labelRect.y += 4;

            editRect = typeRect;
            editRect.x = position.x + position.width - 44;
            editRect.y += 1;
            editRect.width = 40;
            editRect.height = 16;
        }

        protected override void OnGUI(Rect position, GUIContent label)
        {
            Init();

            BeginBlock(metadata, position, GUIContent.none);

            CreateRects(position, label);

            if (nest.hasOptionalOverride)
            {
                nest.isOverridden = GUI.Toggle(toggleRect, nest.isOverridden, GUIContent.none);
            }

            HUMEditor.Disabled(!nest.isOverridden && nest.hasOptionalOverride, () =>
            {
                GUI.Label(labelRect, label);

                GUI.Box(backgroundRect, GUIContent.none, new GUIStyle(EditorStyles.helpBox));
                if (nest.isSpecial) GUI.DrawTexture(iconRect, Images.special_16);

                EditorGUI.BeginDisabledGroup(nest.hasOptionalOverride);
                LudiqGUI.Inspector(metadata["returnType"], typeRect, GUIContent.none);
                EditorGUI.EndDisabledGroup();

                EditButton(editRect);
            });

            if (EndBlock(metadata)) metadata.RecordUndo();
        }

        private void EditButton(Rect position)
        {
            if (GUI.Button(position, "Edit"))
            {
                GraphWindow.OpenActive(GraphReference.New((MethodMacro)macro.value, true));
            }
        }
    }
}