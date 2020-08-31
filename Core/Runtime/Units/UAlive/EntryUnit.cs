using Ludiq;
using Bolt;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Lasm.OdinSerializer;

namespace Lasm.UAlive
{
    [TypeIcon(typeof(Flow))]
    [UnitTitle("Entry")]
    [SpecialUnit][Serializable]
    public class EntryUnit : Unit
    {
        [DoNotSerialize]
        public CustomClass @class;

        [DoNotSerialize]
        public Method root;

        [Inspectable]
        [Serialize]
        public MethodDeclaration declaration = new MethodDeclaration();

        public List<object> assignedValues = new List<object>();

        [DoNotSerialize][PortLabelHidden]
        public ControlOutput invoke;   

        [DoNotSerialize]
        public List<ValueOutput> _outputs = new List<ValueOutput>();

        [OdinSerialize]
        public Action<object> returnMethod;

        [Serialize]
        public List<ReturnUnit> returns = new List<ReturnUnit>();

        
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

            if (root != null) root.entry = this;

            if (declaration.parameters?.Length > 0)
            {
                for (int i = 0; i < declaration.parameters.Length; i++)
                {
                    if (string.IsNullOrEmpty(declaration.parameters[i].name))
                    {
                        var output = ValueOutput(declaration.parameters[i].type, declaration.parameters[i].name);
                        _outputs.Add(output);
                    }
                }
            }

            DefineReturns();
            onChanged?.Invoke();
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