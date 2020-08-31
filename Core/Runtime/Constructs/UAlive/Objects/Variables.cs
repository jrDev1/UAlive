using Lasm.OdinSerializer;
using Ludiq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lasm.UAlive
{
    [Serializable]
    [IncludeInSettings(false)]
    public class Variables
    {
        [Serialize]
        public List<Variable> variables = new List<Variable>();

        public T Get<T>(string name)
        {
            for (int i = 0; i < variables.Count; i++)
            {
                if (variables[i].name == name) return (T)variables[i].declaration.defaultValue;
            } 

            return default(T);
        }

        public object Get(string name)
        {
            for (int i = 0; i < variables.Count; i++)
            {
                if (variables[i].name == name) return variables[i].declaration.defaultValue;
            }

            return null;
        }

        internal bool Has(string name)
        {
            for (int i = 0; i < variables.Count; i++)
            {
                if (variables[i].name == name) return true;
            }

            return false;
        }

        public void Set(string name, object value)
        {
            var index = 0;

            for (int i = 0; i < variables.Count; i++)
            {
                if (variables[i].name == name)
                {
                    index = i;
                    break; ;
                }
            }

            variables[index].declaration.defaultValue = value;
        }
    }
}