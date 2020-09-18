using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using Bolt;

namespace Lasm.UAlive
{
    [Descriptor(typeof(Method))]
    public class MethodDescriptor : MacroDescriptor<Method, MacroDescription>
    {
        public MethodDescriptor(Method target) : base(target)
        {
        }

        public override string Title()
        {
            return target?.entry?.@class?.name.LegalMemberName() + " > " + target.name;
        }
    }
}