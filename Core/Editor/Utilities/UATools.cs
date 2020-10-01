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
        private bool preferencesIsOpen;
        [SerializeField]
        private bool searchIsOpen;
        [SerializeField]
        private bool explorerIsOpen;
        private bool autoSave;
        private int autoSaveRate = 120;

        [MenuItem("Window/UAlive/Tools")]
        public static void Open()
        {
            current = GetWindow<UATools>();
            current.titleContent = new UnityEngine.GUIContent("UA Tools");
        }

        private void OnEnable()
        {
            EnsurePref("UAlive_AutoSave", PrefType.Bool);
            EnsurePref("UAlive_AutoSaveRate", PrefType.Int);
            autoSave = EditorPrefs.GetBool("UAlive_AutoSave");
            autoSaveRate = EditorPrefs.GetInt("UAlive_AutoSaveRate");
            if (autoSaveRate == 0) autoSaveRate = 120;
        }

        private void OnGUI()
        {
            Images.Cache();

            compilationIsOpen = HUMEditor.Foldout(compilationIsOpen, new GUIContent("Compilation", Images.compilation_16), Styles.backgroundColor, Styles.borderColor, 1, () =>
            {
                HUMEditor.Vertical().Box(Styles.backgroundColor.Brighten(0.06f), Styles.borderColor, new RectOffset(6, 6, 6, 6), new RectOffset(1, 1, 0, 1), () => { LiveStatus(); });
            });

            preferencesIsOpen = HUMEditor.Foldout(preferencesIsOpen, new GUIContent("Preferences", Images.settings_16), Styles.backgroundColor, Styles.borderColor, 1, () =>
            {
                HUMEditor.Vertical().Box(Styles.backgroundColor.Brighten(0.06f), Styles.borderColor, new RectOffset(6, 6, 6, 6), new RectOffset(1, 1, 0, 1), () => { GlobalSettings(); });
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

        private void GlobalSettings()
        {
            autoSave = GUILayout.Toggle(autoSave, "Auto Save");
            autoSaveRate = EditorGUILayout.IntSlider("Save Rate (Seconds)", autoSaveRate, 10, 600);
            if (EditorPrefs.GetBool("UAlive_AutoSave") != autoSave) EditorPrefs.SetBool("UAlive_AutoSave", autoSave);
            if (EditorPrefs.GetInt("UAlive_AutoSaveRate") != autoSaveRate) EditorPrefs.SetInt("UAlive_AutoSaveRate", autoSaveRate);
        }

        public static void EnsurePref(string key, PrefType type)
        {
            if (!EditorPrefs.HasKey(key))
            {
                if (type == PrefType.Bool) EditorPrefs.SetBool(key, false);
                if (type == PrefType.Float) EditorPrefs.SetFloat(key, 0f);
                if (type == PrefType.Int) EditorPrefs.SetInt(key, 0);
                if (type == PrefType.String) EditorPrefs.SetString(key, string.Empty);
            }
        }

        public enum PrefType
        {
            Bool,
            Float,
            Int,
            String
        }
    }
}
