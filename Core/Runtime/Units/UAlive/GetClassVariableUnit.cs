using Bolt;
using Lasm.OdinSerializer;
using Ludiq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lasm.UAlive
{
    [UnitTitle("Get Class Variable")]
    [UnitCategory("Variables")]
    [UnitOrder(100)]
    [TypeIcon(typeof(CustomClass))]
    public sealed class GetClassVariableUnit : ClassVariableUnit
    {
        [DoNotSerialize]
        public Variable variable;
         
        [DoNotSerialize][PortLabelHidden]
        public ValueOutput value;
        
        protected override void Definition()
        {
            base.Definition();

            if (id != 0)
            {
                variable = FindWithID(id); 

                value = ValueOutput(variable.type, "value", (flow) =>
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
            }

            if (variable != null)
            {
                Requirement(target, value);
            }
        }
        
        protected override void AfterDefine()
        {
            if (variable != null) variable.onChanged += Define;
        }

        protected override void BeforeUndefine()
        {
            if (variable != null) variable.onChanged -= Define;
        }
    }
}  