using Bolt;
using Lasm.OdinSerializer;
using Ludiq;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace Lasm.UAlive
{
    [Serializable]
    public abstract class ClassMemberUnit : Unit
    {  
        [DoNotSerialize]
        [AllowsNull]
        public ValueInput target;
        [Serialize]
        public CustomClass Class;

        protected override void Definition()
        {
            target = ValueInput<IUAClass>("target").AllowsNull();
        }

        protected IUAClass GetTarget(Flow flow)
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

            return _target;
        }
    }
}

