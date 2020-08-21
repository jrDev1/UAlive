using Ludiq;
using System;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    [Serializable]
    [Inspectable]
    public sealed class InterfaceMethodItem
    {
        [Inspectable]
        public string name;
        [Inspectable]
        public Type returnType = typeof(object);
        [Inspectable]
        [InspectorWide]
        public Dictionary<string, Type> parameters = new Dictionary<string, Type>();
    }
}