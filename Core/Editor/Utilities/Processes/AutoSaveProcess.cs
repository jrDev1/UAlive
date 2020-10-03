#if UNITY_EDITOR
using UnityEditor;

namespace Lasm.UAlive
{
    public sealed class AutoSaveProcess : GlobalProcess
    {
        public static float startTime;
        public static float currentTime;

        public override void Process()
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

        public override void OnBind()
        {
            startTime = (float)EditorApplication.timeSinceStartup;
        }
    }
}
#endif