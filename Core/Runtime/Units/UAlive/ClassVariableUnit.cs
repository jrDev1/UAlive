using Bolt;
using Ludiq;
using System;
using System.Linq;

namespace Lasm.UAlive
{
    [UnitCategory("Codebase")]
    public abstract class ClassVariableUnit : ClassMemberUnit
    {
        [Serialize]
        public Variable variable;

        public ClassVariableUnit() : base() { }

        public ClassVariableUnit(Variable variable, CustomClass @class) : base(@class)
        {
            this.variable = variable;
        }
    }
}