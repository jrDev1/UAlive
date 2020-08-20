using Ludiq;
using System;
using UnityEditor;

namespace Lasm.UAlive
{
    [Serializable]
    public sealed class Method
    {
        public string name { get => nest.name; set { nest.name = value; nest.macro.name = value; } }
        [InspectorLabel(null)]
        public FlowNest nest = new FlowNest();
    }
}