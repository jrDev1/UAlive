using Bolt;
using Ludiq;
using System;

namespace Lasm.UAlive
{
    public abstract class MethodUnit : ClassMemberUnit
    {
        [Serialize]
        public Method method;

        public MethodUnit() : base() { }

        public MethodUnit(CustomClass @class, Method method) : base(@class)
        {
            this.method = method;
        }
    }
}

