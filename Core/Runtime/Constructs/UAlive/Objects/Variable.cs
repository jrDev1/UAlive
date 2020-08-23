using Ludiq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lasm.UAlive
{
    [Serializable][Inspectable]
    public sealed class Variable 
    {
        [Inspectable]
        public string name;

        [Serialize]
        public List<GetClassVariableUnit> getUnits = new List<GetClassVariableUnit>();
        [Serialize]
        public List<SetClassVariableUnit> setUnits = new List<SetClassVariableUnit>();

        [Serialize]
        private Type _type = typeof(int);
        public Type type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    this.value = value.Default();
                    Changed();
                }

                _type = value;
            }
        }

        [Inspectable][Serialize]
        public object value = 0;

        public Method getter = new Method();
        public Method setter = new Method();

        public event Action onChanged = () => { };
         
        public void Changed()
        {
            for (int i = 0; i < getUnits.Count; i++)
            {
                getUnits[i].Define();
            }
        }

        public void OnBeforeSerialize()
        {
            getUnits.Clear();
        }

        public void OnAfterDeserialize()
        {
        }
    }
}