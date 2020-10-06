using Bolt;
using Lasm.OdinSerializer;
using Ludiq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Lasm.UAlive
{
    [Serializable]
    [UnitCategory("Codebase")]
    public sealed class GetClassVariableUnit : ClassVariableUnit
    {
        [DoNotSerialize] [PortLabelHidden]
        public ValueOutput value;
        public string TypeName;
        private Type type;

        public GetClassVariableUnit() : base()
        {
        }

        public GetClassVariableUnit(Variable variable, CustomClass @class) : base(variable, @class)
        {
            TypeName = variable.declaration.type.FullName;
        }

        protected override void Definition()
        {
            base.Definition();

            type = variable.declaration.type;

            value = ValueOutput(type, "value", (flow) =>
            {
                return GetTarget(flow)?.Class?.Get(variable); 
            });

            Requirement(target, value);
        }
        
        public void UpdateType()
        {
            TypeName = variable.declaration.type.AssemblyQualifiedName;
        }

        protected override void AfterDefine()
        {
            if (graph != null && variable != null && variable.declaration != null)
            {
                variable.declaration.onChanged += UpdateType;
                variable.declaration.onChanged += Define;
            }
        }

        protected override void BeforeUndefine()
        {
            if (graph != null && variable != null && variable.declaration != null)
            {
                variable.declaration.onChanged -= UpdateType;
                variable.declaration.onChanged -= Define;
            }
        }
    }
}  