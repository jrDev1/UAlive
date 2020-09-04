using UnityEditor;
using UnityEngine;

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
            EditorApplication.delayCall += DelayInitialize;
        }

        private static void DelayInitialize()
        {
            var macros = HUMAssets.Find().Assets().OfType<IDefinable>();

            for (int i = 0; i < macros.Count; i++)
            {
                macros[i].Define();
            }
        }
        
        public static void Disable()
        {
            if (isInitializing) EditorApplication.update -= DelayInitialize;
        }
    }
}