using Bolt;
using Ludiq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lasm.UAlive
{
    [UnitOrder(100)]
    [RenamedFrom("Lasm.BoltExtensions.ValueReroute")]
    public sealed class ValueReroute : Unit
    {
        [DoNotSerialize][PortLabelHidden]
        public ValueInput input;
        [DoNotSerialize][PortLabelHidden]
        public ValueOutput output;
        public Type portType = typeof(object);

        protected override void Definition()
        {
            input = ValueInput(portType, "in");
            output = ValueOutput(portType, "out", (flow) => { return flow.GetValue(input); });
            Requirement(input, output);
        }
    }
}