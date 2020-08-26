using Ludiq;
using System;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    [Serializable]
    [IncludeInSettings(false)]
    public sealed class ClassVariables
    {
        [Serialize]
        internal Dictionary<string, object> variables = new Dictionary<string, object>();

        internal T Get<T>(CustomClass macro, string name)
        {
            return (T)Get(macro, name);
        }

        internal object Get(CustomClass macro, string name)
        {
            if (variables.ContainsKey(name))
            {
                return variables[name];
            }

            if (macro.variables.Has(name))
            {
                variables.Add(name, macro.variables.Get(name));
                return variables[name];
            }

            throw new NullReferenceException("The class '" + macro.title + "' does not contain the a variable named '" + name + "'.");
        }

        internal void Set(CustomClass macro, string name, object value)
        {
            if (variables.ContainsKey(name))
            {
                variables[name] = value;
                return;
            }

            if (macro.variables.Has(name))
            {
                variables.Add(name, value);
            }
        }
    }
}