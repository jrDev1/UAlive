using Bolt;
using Ludiq;
using System;

namespace Lasm.UAlive
{
    public abstract class MethodUnit : ClassMemberUnit
    {
        [Serialize]
        public Method method;

        public MethodUnit() { }

        public MethodUnit(CustomClass @class, Method method)
        {
            this.Class = @class;
            this.method = method;
        }
    }
}

