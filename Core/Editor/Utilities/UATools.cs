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
}
