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

        [Serialize] 
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

        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput outValue;

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
                outValue = ValueOutput(variable.declaration.type, "valueOut", (flow)=> 
                {
                    return GetTarget(flow)?.Class?.Get(variable);
                });
                value.SetDefaultValue(variable.declaration.type.Default());
            }

            enter = ControlInput("enter", (flow) =>
            {
                GetTarget(flow)?.Class?.Set(variable, flow.GetValue(value, variable.declaration.type));
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