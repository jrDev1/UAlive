using Ludiq;
using System;
using System.Linq;
using UnityEditor;

namespace Lasm.UAlive
{
    [Serializable]
    [IncludeInSettings(false)]
    public abstract class TypeMacro : LudiqScriptableObject
    {
        public string title;
        public string @namespace;

#if UNITY_EDITOR
        public bool isLive;

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
            HUMFlow.AfterTicks(ref ticks, 4, afterTicks: () =>
            {
                var macros = HUMAssets.Find().Assets().OfType<TypeMacro>();
                for (int i = 0; i < macros.Count; i++)
                {
                    macros[i].Define();
                }
                EditorApplication.update -= DelayInitialize;
                isInitializing = false;
            });
        }

        private void OnDisable()
        {
            if (isInitializing) EditorApplication.update -= DelayInitialize;
        }
#endif

        public static T CreateAsset<T>(string defaultName = "") where T : TypeMacro
        {
            var macro = CreateInstance<T>();
            macro.title = defaultName;
            AssetDatabase.CreateAsset(macro, HUMAssets.ProjectWindowPath() + "/" + macro.title.Add().Space().Between().Lowercase().And().Uppercase() + " " + macro.GetInstanceID().ToString().Replace("-", string.Empty) + ".asset");
            macro.Define();
            return macro;
        }

        public void Define()
        {
            BeforeDefine();
            Definition();
            AfterDefine();
            RefreshOnChange();
        }

        protected virtual void BeforeDefine()
        {
        }

        protected virtual void AfterDefine()
        {
        }

        protected abstract void Definition();

        protected virtual void RefreshOnChange()
        {
        }


        protected string GetDefaultName()
        {
            return this.title + this.GetInstanceID().ToString().Replace("-", string.Empty);
        }
    }
}