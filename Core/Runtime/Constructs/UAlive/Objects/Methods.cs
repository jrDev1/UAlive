using Lasm.OdinSerializer;
using Ludiq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    [Serializable]
    public sealed class Methods
    {
        [Serialize]
        public MethodDictionary overrides = new MethodDictionary();

        [OdinSerialize]
        public List<Method> custom = new List<Method>();
    }
}