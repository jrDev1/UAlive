using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using Bolt;

namespace Lasm.UAlive
{
    [Descriptor(typeof(FlowNestMacro))]
    public class FlowNestMacroDescriptor : MacroDescriptor<FlowNestMacro, MacroDescription>
    {
        public FlowNestMacroDescriptor(FlowNestMacro target) : base(target)
        {
        }
    }
}