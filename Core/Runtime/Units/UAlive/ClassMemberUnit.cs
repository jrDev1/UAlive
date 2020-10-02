using Bolt;
using Lasm.OdinSerializer;
using Ludiq;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    public abstract class ClassMemberUnit : Unit
    {  
        [DoNotSerialize]
        [AllowsNull]
        [NullMeansSelf]
        public ValueInput target;
        [Serialize]
        public CustomClass Class;
        protected Type castedType;

        public ClassMemberUnit() : base()
        {

        }

        public ClassMemberUnit(CustomClass Class)
        {
            this.Class = Class;
        }

        protected override void Definition()
        {
            if (Class != null)
            {
                castedType = Class.inheritance.compiledType;

                if (castedType == null)
                {
                    target = ValueInput<IUAClass>("target").AllowsNull();
                }
                else
                {
                    target = ValueInput(castedType, "target").AllowsNull();

                    if (castedType.Inherits<MonoBehaviour>())
                    {
                        target.SetDefaultValue(null);
                        target.NullMeansSelf();
                    }
                    target.SetDefaultValue(castedType.Default());
                }

                return;
            }

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

