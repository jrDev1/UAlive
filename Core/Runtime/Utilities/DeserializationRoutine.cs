#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    public static class DeserializationRoutine
    {
        private static int ticks;
        private static bool isInitializing;
        private static GlobalUpdate update = new GlobalUpdate();

        [InitializeOnLoadMethod]
        private static void StartInitializing()
        {
            EditorApplication.delayCall += DelayInitialize;
        }

        private static void DelayInitialize()
        {
            isInitializing = true;

            update.Bind();

#if UNITY_EDITOR
            var macros = HUMAssets.Find().Assets().OfType<IDefinable>();
#else
            var macros = 
#endif
            for (int i = 0; i < macros.Count; i++)
            {
                macros[i].Define();
            }
        }
        
        public static void Disable()
        {
            if (isInitializing)
            {
                EditorApplication.update -= DelayInitialize;
                update.Unbind();
            }
        }
    }
}
#endif