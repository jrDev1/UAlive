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
    [UnitCategory("Codebase")]
    [TypeIcon(typeof(CustomClass))]
    [Serializable]
    public sealed class GetClassVariableUnit : ClassVariableUnit
    {
        [DoNotSerialize] [PortLabelHidden]
        public ValueOutput value;

        public GetClassVariableUnit() : base()
        {
        }

        public GetClassVariableUnit(Variable variable, CustomClass @class) : base(variable, @class)
        {
        }

        protected override void Definition()
        {
            base.Definition();

            if (variable?.declaration?.type != null) 
            {
                value = ValueOutput(variable.declaration.type, "value", (flow) =>
                {
                    return GetTarget(flow)?.Class?.Get(variable); 
                });

                Requirement(target, value);
            }
        }
        
        protected override void AfterDefine()
        {
            if (graph != null && variable != null && variable.declaration != null) variable.declaration.onChanged += Define;
        }

        protected override void BeforeUndefine()
        {
            if (graph != null && variable != null && variable.declaration != null) variable.declaration.onChanged -= Define;
        }
    }
}  