using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    public sealed class UATools : EditorWindow
    {
        public static UATools current;
        [SerializeField]
        private bool compilationIsOpen;
        [SerializeField]
        private bool searchIsOpen;
        [SerializeField]
        private bool explorerIsOpen;

        [MenuItem("Window/UAlive/Tools")]
        public static void Open()
        {
            current = GetWindow<UATools>();
            current.titleContent = new UnityEngine.GUIContent("UA Tools");
        }

        private void OnGUI()
        {
            Images.Cache();

            compilationIsOpen = HUMEditor.Foldout(compilationIsOpen, new GUIContent("Compilation", Images.compilation_16), Styles.backgroundColor, Styles.borderColor, 1, () =>
            {
                HUMEditor.Vertical().Box(Styles.backgroundColor.Brighten(0.06f), Styles.borderColor, new RectOffset(6, 6, 6, 6), new RectOffset(1, 1, 0, 1), () => { LiveStatus(); });
            });

            searchIsOpen = HUMEditor.Foldout(searchIsOpen, new GUIContent("Search", Images.search_16), Styles.backgroundColor, Styles.borderColor, 1, () =>
            {
                EditorGUILayout.HelpBox("The search and replace functionality is not available at this time.", MessageType.Info);
            });

            explorerIsOpen = HUMEditor.Foldout(explorerIsOpen, new GUIContent("Explorer", Images.explorer_16), Styles.backgroundColor, Styles.borderColor, 1, () =>
            {
                EditorGUILayout.HelpBox("The project explorer is not available at this time.", MessageType.Info);
            });
        }

        private void LiveStatus()
        {
            HUMEditor.Vertical(() =>
            {
                if (GUILayout.Button("Compile Live C# Code"))
                {
                        MenuCommands.GenerateLive();
                }

                if (GUILayout.Button("Compile Native C# Code"))
                {
                    MenuCommands.GenerateNative();
                }
            });
        }
    }

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
        public static Color construct => new Color(0.2f, 0.2f, 0.8f);
        public static Color normal => new Color(0.85f, 0.85f, 0.85f);

        public static CustomType selection => Selection.activeObject as CustomType;
        public static CustomClass Class => Selection.activeObject as CustomClass;
        public static CustomInterface Interface => Selection.activeObject as CustomInterface;
        public static CustomEnum Enum => Selection.activeObject as CustomEnum;

        private bool changed = true;

        public void Changed() { changed = true; }

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
                    if (changed && selection != null)
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
                        changed = false;
                    }

                    GUILayout.Label(output, new GUIStyle(GUI.skin.label) { stretchWidth = true, stretchHeight = true, alignment = TextAnchor.UpperLeft, wordWrap = true });

                }, true, true);
            });

            Repaint();
        }
    }
}
