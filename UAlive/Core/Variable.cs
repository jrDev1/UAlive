using Ludiq;
using System;
using System.Collections.Generic;

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
        [Inspectable]
        public Type type
        {
            get => _type;
            set
            {
                if (_type != value || this.value == null || this.value.GetType() != value)
                {
                    this.value = value.Default();
                    getter.macro.entry.Define();
                    setter.macro.entry.Define();
                }

                DefineGet();
                DefineSet(); 
                _type = value;
            }
        }

        [Inspectable][Serialize]
        public object value = 0;

        public FlowNest getter = new FlowNest();

        public FlowNest setter = new FlowNest();

        public void DefineGet()
        {
            for (int i = 0; i < getUnits.Count; i++)
            {
                getUnits[i].Define();
            }
        }

        public void DefineSet()
        {
            for (int i = 0; i < setUnits.Count; i++)
            {
                setUnits[i].Define();
            }
        }
    }
}