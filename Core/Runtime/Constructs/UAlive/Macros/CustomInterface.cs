using Ludiq;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace Lasm.UAlive
{
    [Serializable]
    public sealed class CustomInterface : CustomType
    {
        #region Declaration

        public override string GetDefaultName()
        {
            return "New Custom Interface";
        }

        #endregion

        [Inspectable]
        [InspectorWide]
        public List<InterfacePropertyItem> properties = new List<InterfacePropertyItem>();

        [Inspectable]
        [InspectorWide]
        public List<InterfaceMethodItem> methods = new List<InterfaceMethodItem>();

#if UNITY_EDITOR
        public bool methodsOpen;
        public bool propertiesOpen;
#endif
    }
}