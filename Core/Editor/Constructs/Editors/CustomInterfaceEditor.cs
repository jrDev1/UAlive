using UnityEditor;
using UnityEngine;
using Ludiq;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    [Editor(typeof(CustomInterface))]
    public sealed class CustomInterfaceEditor : Inspector
    {
        private CustomInterfaceGenerator generator;
        private CustomInterface _target;
        private Color borderColor => EditorGUIUtility.isProSkin ? HUMColor.Grey(0.1f) : HUMColor.Grey(0.25f);
        private Color backgroundColor => EditorGUIUtility.isProSkin ? HUMEditorColor.DefaultEditorBackground.Darken(0.1f) : HUMEditorColor.DefaultEditorBackground.Darken(0.5f);
        private List<int> indexes = new List<int>();

        public CustomInterfaceEditor(Metadata metadata) : base(metadata)
        {
        }

        protected override float GetHeight(float width, GUIContent label)
        {
            return 0;
        }

        public override void Initialize()
        {
            Images.Cache();
            _target = metadata.value as CustomInterface;
            generator = CustomInterfaceGenerator.GetDecorator(_target);
        }

        protected override void OnGUI(Rect position, GUIContent label)
        {
            BeginBlock(metadata, position, GUIContent.none);

            HUMEditor.Vertical().Box(backgroundColor.Brighten(0.05f), Color.black, new RectOffset(4, 4, 4, 4), new RectOffset(1, 1, 1, 1), () =>
            {
                _target.title = EditorGUILayout.TextField(new GUIContent("Interface Name"), _target.title);
                _target.@namespace = EditorGUILayout.TextField(new GUIContent("Namespace"), _target.@namespace);
            });

            GUILayout.Space(10);

            _target.propertiesOpen = HUMEditor.Foldout(_target.propertiesOpen, new GUIContent("Properties", Images.property_16),
            backgroundColor.Brighten(0.05f),
            Color.black,
            1,
            () =>
            {
                LudiqGUI.InspectorLayout(metadata["properties"], GUIContent.none);
            });

            GUILayout.Space(10);

            _target.methodsOpen = HUMEditor.Foldout(_target.methodsOpen, new GUIContent("Methods", Images.flow_icon_16),
            backgroundColor.Brighten(0.05f),
            Color.black,
            1,
            () =>
            {
                LudiqGUI.InspectorLayout(metadata["methods"], GUIContent.none);
            });

            if (EndBlock(metadata))
            {
                metadata.RecordUndo();
            }
        }
    }
}