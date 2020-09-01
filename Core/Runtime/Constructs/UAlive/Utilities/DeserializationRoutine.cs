using UnityEditor;

namespace Lasm.UAlive
{
    public static class DeserializationRoutine
    {
        private static int ticks;
        private static bool isInitializing;

        [InitializeOnLoadMethod]
        private static void StartInitializing()
        {
            isInitializing = true;
            EditorApplication.update += DelayInitialize;
        }

        private static void DelayInitialize()
        {
            HUMFlow.AfterTicks(ref ticks, 2, afterTicks: () =>
            {
                var macros = HUMAssets.Find().Assets().OfType<IDefinable>();
                for (int i = 0; i < macros.Count; i++)
                {
                    macros[i].Define();
                }
                EditorApplication.update -= DelayInitialize;
                isInitializing = false;
            });
        }
        
        public static void Disable()
        {
            if (isInitializing) EditorApplication.update -= DelayInitialize;
        }
    }
}