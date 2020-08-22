using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using Bolt;

namespace Lasm.UAlive
{
    [Descriptor(typeof(MethodMacro))]
    public class MethodMacroDescriptor : MacroDescriptor<MethodMacro, MacroDescription>
    {
        public MethodMacroDescriptor(MethodMacro target) : base(target)
        {
        }
    }
}