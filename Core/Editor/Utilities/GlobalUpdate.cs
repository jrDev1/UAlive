#if UNITY_EDITOR
using System;
using UnityEditor;

namespace Lasm.UAlive
{
    public sealed class GlobalUpdate
    {
        private bool isInitialized;
        public static event Action processes = ()=> { };
        public static float startTime;
        public static float currentTime;

        public void Bind()
        {
            if (!isInitialized)
            {
                EditorApplication.update += UpdateProcess;
                startTime = (float)EditorApplication.timeSinceStartup;
                processes += AutoSaveProcess;
                isInitialized = true;
            }
        }

        public void Unbind()
        {
            if (isInitialized)
            {
                EditorApplication.update -= UpdateProcess;
                processes -= AutoSaveProcess;
                isInitialized = false;
            } 
        }

        private void UpdateProcess()
        {
            processes();
        }

        private void AutoSaveProcess()
        {
            if (EditorPrefs.GetBool("UAlive_AutoSave"))
            {
                var elapsed = EditorApplication.timeSinceStartup - startTime;
                if (elapsed > EditorPrefs.GetInt("UAlive_AutoSaveRate"))
                {
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    startTime = (float)EditorApplication.timeSinceStartup;
                }
            }
        }
    }
}
#endif