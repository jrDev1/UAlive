using Bolt;
using Ludiq;
using System.Linq;

namespace Lasm.UAlive
{
    public abstract class ClassVariableUnit : ClassMemberUnit
    {
        protected Variable FindWithID(int id)
        {
            if (macro.variables.variables.Any((v) => { return v.id == id; }))
            {
                return macro.variables.variables.Single((v) => { return v.id == id; });
            }

            return null;
        }
    }
}