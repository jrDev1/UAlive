using Ludiq;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace Lasm.UAlive
{
    [Serializable]
    public sealed class InterfaceMacro : TypeMacro
    {
        [Inspectable]
        [InspectorWide]
        public List<InterfacePropertyItem> properties = new List<InterfacePropertyItem>();

        [Inspectable]
        [InspectorWide]
        public List<InterfaceMethodItem> methods = new List<InterfaceMethodItem>();

#if UNITY_EDITOR
        public bool methodsOpen;
        public bool propertiesOpen;

        [MenuItem("Assets/Create/UAlive/Custom Interface", priority = 0)]
        private static void CreateEnumMacro()
        {
            var macro = CreateAsset<InterfaceMacro>("Custom Interface");
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