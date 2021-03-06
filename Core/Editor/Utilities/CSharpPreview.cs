﻿using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    public sealed class CSharpPreview : EditorWindow
    {
        public static CSharpPreview instance;

        string output = string.Empty;
        [SerializeField]
        private Vector2 scrollPosition;

        [MenuItem("Window/UAlive/C# Preview")]
        private static void Open()
        {
            CSharpPreview window = GetWindow<CSharpPreview>();
            window.titleContent = new GUIContent("C# Preview");
            instance = window;
        }

        public static Color background => Styles.backgroundColor.Darken(0.1f);

        public static CustomType selection => Selection.activeObject == null ? null : Selection.activeObject as CustomType;
        public static CustomClass Class => Selection.activeObject == null ? null : Selection.activeObject as CustomClass;
        public static CustomInterface Interface => Selection.activeObject == null ? null : Selection.activeObject as CustomInterface;
        public static CustomEnum Enum => Selection.activeObject == null ? null : Selection.activeObject as CustomEnum;

        private void OnEnable()
        {
            instance = this;
        }

        private void OnGUI()
        {
            scrollPosition = HUMEditor.Draw().ScrollView(scrollPosition, () =>
            {
                HUMEditor.Vertical().Box(background, 10, () =>
                {
                    if (selection != null)
                    {
                        if (Class != null)
                        {
                            output = CustomClassGenerator.GetDecorator(Class).GetCompiledOutput();
                        }
                        else if (Interface != null)
                        {
                            output = CustomInterfaceGenerator.GetDecorator(Interface).GetCompiledOutput();
                        }
                        else
                        {
                            if (Enum != null) output = CustomEnumGenerator.GetDecorator(Enum).GetCompiledOutput();
                        }
                    }

                    output = output.Replace("/*", "<color=#CC3333>/*");
                    output = output.Replace("*/", "*/</color>");
                    var labelStyle = new GUIStyle(GUI.skin.label) { richText = true, stretchWidth = true, stretchHeight = true, alignment = TextAnchor.UpperLeft, wordWrap = true };
                    labelStyle.normal.background = null;
                    GUILayout.Label(output.RemoveMarkdown(), labelStyle);

                }, true, true);
            });

            Repaint();
        }
    }
}
