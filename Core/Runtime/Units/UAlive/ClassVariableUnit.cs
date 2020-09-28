using Bolt;
using Ludiq;
using System;
using System.Linq;

namespace Lasm.UAlive
{
    [Serializable]
    [UnitCategory("Codebase")]
    public abstract class ClassVariableUnit : ClassMemberUnit
    {
        [Serialize]
        public Variable variable;

        public ClassVariableUnit() { }

        public ClassVariableUnit(Variable variable, CustomClass @class)
        {
            this.variable = variable;
            Class = @class;
        }
    }
}