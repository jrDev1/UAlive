using Ludiq;
using Bolt;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lasm.UAlive
{
    [TypeIcon(typeof(Flow))]
    [UnitTitle("Entry")]
    [SpecialUnit]
    public class EntryUnit : Unit
    {
        [Serialize]
        public MethodMacro macro;
        [Inspectable]
        public Dictionary<string, Type> parameters = new Dictionary<string, Type>();
        public List<object> assignedValues = new List<object>();
        [DoNotSerialize][PortLabelHidden]
        public ControlOutput invoke;
        [DoNotSerialize]
        public List<ValueOutput> _outputs = new List<ValueOutput>();
        [OdinSerializer.OdinSerialize]
        public Action<object> returnMethod;
        [Serialize]
        public List<ReturnUnit> returns = new List<ReturnUnit>();
        [Serialize]
        public Type _type;
        public Type returnType
        {
            get => _type;
            set
            {
                _type = value;
                onChanged?.Invoke();
            }
        }

        [Serialize]
        public List<InvokeUnit> invokes = new List<InvokeUnit>();

        public event Action onChanged = () => { };

        public void Changed()
        {
            onChanged?.Invoke();
        }

        protected override void Definition()
        {
            isControlRoot = true;

            invoke = ControlOutput("invoke");

            _outputs.Clear();

            if (parameters.Keys != null)
            {
                if (parameters.Values != null)
                {
                    var keys = parameters.Keys.ToListPooled();
                    var values = parameters.Values.ToListPooled();

                    for (int i = 0; i < keys.Count; i++)
                    {
                        var value = values[i];

                        if (value != null) {
                            var output = ValueOutput(value, keys[i]);
                            _outputs.Add(output);
                        }
                    }
                } 
            }

            DefineReturns();
            onChanged?.Invoke();
        }

        public void DefineInvokes()
        {
            for (int i = 0; i < invokes.Count; i++)
            {
                invokes[i].Define();
            }
        }

        public void DefineReturns()
        {
            for(int i = 0; i < returns.Count; i++)
            {
                returns[i].Define();
            }
        }

        public void AddReturn(ReturnUnit returnUnit)
        {
            returns.Add(returnUnit);
            DefineReturns();
        }
    }
}