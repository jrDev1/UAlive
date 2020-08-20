using Ludiq;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    [Serializable]
    public sealed class EnumMacro : TypeMacro
    {
        [Inspectable]
        public List<EnumItem> items = new List<EnumItem>();

#if UNITY_EDITOR
        [MenuItem("Assets/Create/UAlive/Custom Enum", priority = 0)]
        private static void CreateEnumMacro()
        {
            var macro = CreateAsset<EnumMacro>("Custom Enum");
            if (macro != null)
            {
                Selection.activeObject = macro;
            }
        }
#endif

        protected override void Definition()
        {
            
        }
    }
}