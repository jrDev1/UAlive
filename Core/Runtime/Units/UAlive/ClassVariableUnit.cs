using Bolt;
using Ludiq;
using System.Linq;

namespace Lasm.UAlive
{
    public abstract class ClassVariableUnit : ClassMemberUnit
    {
        protected Variable FindWithID(string guid)
        {
            if (Class.variables.variables.Any((variable) => { return variable.declaration.guid == _guid; }))
            {
                return Class.variables.variables.Single((variable) => { return variable.declaration.guid == _guid; });
            }

            return null;
        }
    }
}