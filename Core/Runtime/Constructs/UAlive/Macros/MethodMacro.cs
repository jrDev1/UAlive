using UnityEngine;
using Ludiq;
using Bolt;
using System;
using System.Collections.Generic;
using Lasm.OdinSerializer;
using UnityEditor;

namespace Lasm.UAlive
{
    [Serializable]
    public class MethodMacro : Macro<FlowGraph>
    {
        [Serialize]
        public EntryUnit entry;
        public bool hasOverride;
        public bool isOverridden;
        public bool isSpecial;

        public override FlowGraph DefaultGraph()
        {
            return new FlowGraph();
        }
         
        public static MethodMacro Create(Action<object> returnMethod = null)
        {
            var macro = ScriptableObject.CreateInstance<MethodMacro>();
            macro.graph = new FlowGraph();
            macro.entry = new EntryUnit();
            macro.entry.macro = macro;
            macro.graph.units.Add(macro.entry);
            macro.entry.returnMethod = returnMethod; 
            return macro;    
        }
    }
}