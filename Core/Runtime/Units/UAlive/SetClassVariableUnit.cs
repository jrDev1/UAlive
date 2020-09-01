using Bolt;
using Ludiq;
using System;
using System.Linq;
using UnityEngine;

namespace Lasm.UAlive
{
    [UnitTitle("Set Class Variable")]
    [UnitOrder(100)]
    [UnitCategory("Variables")]
    [Serializable]
    public sealed class SetClassVariableUnit : ClassVariableUnit
    {
        [Serialize]
        [Inspectable]
        public bool chain;

        [SerializeField] 
        public Variable variable;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput enter;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput exit;

        [DoNotSerialize] 
        [PortLabelHidden]
        public ValueInput value;

        private bool justDefined;

        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput chainTarget;

        private object returnValue;

        protected override void Definition()
        {
            base.Definition(); 

            if (chain) chainTarget = ValueOutput<IUAClass>("chain", (flow) => { return flow.GetValue<IUAClass>(target); });

            if (variable != null)
            {
                value = ValueInput(variable.declaration.type, "value");
            }

            enter = ControlInput("enter", (flow) =>
            {
                IUAClass _target;

                if (target.hasValidConnection)
                {
                    _target = flow.GetValue<IUAClass>(target);
                }
                else
                { 
                    _target = (IUAClass)flow.variables.Get("#secret_uaclass_instance");
                }

                _target.Class.Set(variable.name, flow.GetValue(value, variable.declaration.type));
                return exit;
            });

            exit = ControlOutput("exit");

            Requirement(target, enter);
            Succession(enter, exit);
        }

        protected override void AfterDefine()
        {
            if (variable != null) variable.declaration.onChanged += Define;
        }

        protected override void BeforeUndefine()
        {
            if (variable != null) variable.declaration.onChanged -= Define;
        }
    }
}