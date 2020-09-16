using Ludiq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lasm.UAlive
{
    [Serializable]
    [IncludeInSettings(true)]
    [Inspectable]
    public sealed class UAClass
    {
        [InspectorWide]
        [Serialize]
        public List<RuntimeVariable> variables = new List<RuntimeVariable>();

        [Serialize]
        public CustomClass macro;
        public string GUID { get; }

        public UAClass(string GUID)
        {
            this.GUID = GUID;
        }

        public void EnsureInitialized(string GUID)
        {
            ClassExtensions.GetClass(ref macro, GUID);
        }

        public void Invoke(IUAClass @class, string name, Action<object> returnMethod, bool isOverride = false, params object[] parameters)
        {
            EnsureInitialized(GUID);
            ClassExtensions.Invoke(@class, name, returnMethod, isOverride, parameters);
        }

        public void Refresh(CustomClass macro)
        {
            var macroVariables = macro.variables.variables;

            for (int i = 0; i < macroVariables.Count; i++)
            {
                if (GetRuntimeVariable(macroVariables[i]) == null)
                {
                    variables.Add(new RuntimeVariable() { referenceGUID = macroVariables[i].GetGUID(), value = macroVariables[i].declaration.defaultValue, reference = macroVariables[i], classReference = macro });
                }
            }

            RuntimeVariable removed = null;

            for (int i = 0; i < variables.Count; i++)
            {
                if (GetClassVariable(variables[i]) == null)
                {
                    removed = variables[i];
                    break;
                }
            }

            if (removed != null) variables.Remove(removed);
        }

        public RuntimeVariable GetRuntimeVariable(Variable classVariable)
        {
            for (int i = 0; i < variables.Count; i++)
            {
                if (classVariable?.name == variables[i].reference?.name)
                {
                    return variables[i];
                }
            }

            return null;
        }

        public Variable GetClassVariable(RuntimeVariable runtimeVariable)
        {
            for (int i = 0; i < macro.variables.variables.Count; i++)
            {
                if (macro?.variables?.variables[i]?.name == runtimeVariable?.reference?.name)
                {
                    return macro.variables.variables[i];
                }
            }

            return null;
        }

        public bool Contains(RuntimeVariable variable)
        {
            return macro.variables.variables.Contains(variable.reference);
        }

        public bool AssetContains(Variable variable)
        {
            return macro.variables.variables.Contains(variable);
        }

        public T Get<T>(Variable variable)
        {
            return (T)Get(variable);
        }

        public object Get(Variable variable)
        {
            EnsureInitialized(GUID);
            var runtimeVariable = GetRuntimeVariable(variable);
            if (runtimeVariable == null)
            {
                runtimeVariable = new RuntimeVariable() { referenceGUID = variable.GetGUID(), value = variable.declaration.defaultValue, reference = variable, classReference = macro };
                variables.Add(runtimeVariable);
                return runtimeVariable.value;
            }

            if (runtimeVariable != null)
            {
                return runtimeVariable.value;
            }

            throw new NullReferenceException("The class '" + macro.title + "' does not contain the a variable named '" + variable.name + "'.");
        }

        public void Set(Variable variable, object value)
        {
            var runtimeVariable = GetRuntimeVariable(variable);
            if (runtimeVariable == null)
            {
                runtimeVariable = new RuntimeVariable() { referenceGUID = variable.GetGUID(), value = variable.declaration.defaultValue, reference = variable, classReference = macro };
                variables.Add(runtimeVariable);
            }

            runtimeVariable.value = value;
        }
    }
}