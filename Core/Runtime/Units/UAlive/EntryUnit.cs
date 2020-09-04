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
        [InspectorLabel(null)]
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
            DefineReturns();
            onChanged?.Invoke();
            declaration?.Changed();
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
                    if (!string.IsNullOrEmpty(declaration.parameters[i].name))
                    {
                        var output = ValueOutput(declaration.parameters[i].type, declaration.parameters[i].name);
                        _outputs.Add(output);
                    }
                }
            }

            Changed();
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

        protected override void AfterDefine()
        {
            if (declaration != null) declaration.changed += Define;
        }

        protected override void BeforeUndefine()
        {
            if (declaration != null) declaration.changed -= Define;
        }
    }
}