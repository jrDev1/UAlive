using UnityEngine;
using Ludiq;
using Bolt;
using System;
using System.Collections.Generic;
using Lasm.OdinSerializer;

namespace Lasm.UAlive
{
    [CreateAssetMenu(fileName = "Flow Nest Macro", menuName = "Bolt/Extensions/Flow Nest Macro")]
    public class MethodMacro : Macro<FlowGraph>
    {
        [NonSerialized][OdinSerialize]
        public List<Type> constraints;
        [OdinSerialize]
        public EntryUnit entry;
        [OdinSerialize]
        public Action<object> returnMethod;
        public bool hasOverride;
        public bool isSpecial;

        public override FlowGraph DefaultGraph()
        {
            return new FlowGraph();
        }

        public void Awake()
        {
            if (graph == null) graph = new FlowGraph();
            if (entry == null) entry = new EntryUnit();
            if (entry.macro == null) entry.macro = this;
            if (!graph.units.Contains(entry)) graph.units.Add(entry);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (graph == null) graph = new FlowGraph();
            if (entry == null) entry = new EntryUnit();
            if (entry.macro == null) entry.macro = this;
            if (!graph.units.Contains(entry)) graph.units.Add(entry);
        }
    }
}