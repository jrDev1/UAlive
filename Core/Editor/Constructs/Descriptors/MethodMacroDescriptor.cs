using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using Bolt;

namespace Lasm.UAlive
{
    [Descriptor(typeof(Method))]
    public class MethodMacroDescriptor : MacroDescriptor<Method, MacroDescription>
    {
        public MethodMacroDescriptor(Method target) : base(target)
        {
        }
    }
}