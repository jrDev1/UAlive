using UnityEditor;
using UnityEngine;
using Ludiq;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    [Editor(typeof(EnumMacro))]
    public sealed class EnumMacroEditor : Inspector
    {
        private EnumMacroGenerator generator;
        private EnumMacro _target;
        private Color borderColor => EditorGUIUtility.isProSkin ? HUMColor.Grey(0.1f) : HUMColor.Grey(0.25f);
        private Color backgroundColor => EditorGUIUtility.isProSkin ? HUMEditorColor.DefaultEditorBackground.Darken(0.1f) : HUMEditorColor.DefaultEditorBackground.Darken(0.5f);
        private List<int> indexes = new List<int>();

        public EnumMacroEditor(Metadata metadata) : base(metadata)
        {
        }

        protected override float GetHeight(float width, GUIContent label)
        {
            return 0;
        }

        public override void Initialize()
        {
            Images.Cache();
            _target = metadata.value as EnumMacro;
            generator = EnumMacroGenerator.GetDecorator(_target);
        }

        protected override void OnGUI(Rect position, GUIContent label)
        {
            BeginBlock(metadata, position, GUIContent.none);

            HUMEditor.Vertical().Box(backgroundColor.Brighten(0.05f), Color.black, new RectOffset(4, 4, 4, 4), new RectOffset(1, 1, 1, 1), () =>
            {
                _target.title = EditorGUILayout.TextField(new GUIContent("Enum Name"), _target.title);
                _target.@namespace = EditorGUILayout.TextField(new GUIContent("Namespace"), _target.@namespace);
            });

            GUILayout.Space(10);

            LudiqGUI.InspectorLayout(metadata["items"], GUIContent.none);

            if (EndBlock(metadata))
            {
                metadata.RecordUndo();
            }
        }
    }
}