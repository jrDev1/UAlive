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

            var macros = HUMAssets.Find().Assets().OfType<CustomClass>();

            for (int i = 0; i < macros.Count; i++)
            {
                macros[i].Definer().Define();
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
