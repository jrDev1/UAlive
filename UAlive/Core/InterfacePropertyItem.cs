using Ludiq;
using System;

namespace Lasm.UAlive
{
    [Serializable]
    [Inspectable]
    public sealed class InterfacePropertyItem
    {
        [Inspectable]
        public string name;
        [Inspectable]
        public Type type;
        [Inspectable]
        public bool get;
        [Inspectable]
        public bool set;
    }
}