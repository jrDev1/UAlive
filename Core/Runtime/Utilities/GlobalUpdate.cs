#if UNITY_EDITOR
using System;
using UnityEditor;

namespace Lasm.UAlive
{
    public sealed class GlobalUpdate
    {
        private bool isInitialized;
        public static event Action processes = ()=> { };
        public static int autoSaveRate;
        private static int autoSaveCount = 0;

        public void Bind()
        {
            if (!isInitialized)
            {
                EditorApplication.update += UpdateProcess;

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
            //processes();
        }

        private void AutoSaveProcess()
        {
            if (EditorPrefs.GetBool("UAlive_AutoSave"))
            {
                HUMFlow.AfterTicks(ref autoSaveCount, autoSaveRate, true, () =>
                {
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                });
            }
        }
    }
}
#endif