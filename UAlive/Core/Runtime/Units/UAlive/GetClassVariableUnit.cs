using Bolt;
using Ludiq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lasm.UAlive
{
    [UnitTitle("Get Class Variable")]
    [UnitCategory("Variables")]
    [TypeIcon(typeof(ClassMacro))]
    public sealed class GetClassVariableUnit : ClassVariableUnit
    {
        [Serialize]
        public Variable variable;

        [DoNotSerialize][PortLabelHidden]
        public ValueOutput value;

        private bool justDefined;

        protected override void Definition()
        {
            base.Definition();

            value = ValueOutput(variable == null ? typeof(object) : variable.type, "value", (flow)=> 
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

                return _target.Class.Get(variable.name);
            });

            if (variable != null)
            {
                if (!variable.getUnits.Contains(this)) variable.getUnits.Add(this);
            }

            Requirement(target, value);
        }
    }
}