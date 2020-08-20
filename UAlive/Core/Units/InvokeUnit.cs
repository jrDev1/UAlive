using Bolt;
using Lasm.OdinSerializer;
using Ludiq;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    [UnitTitle("Invoke")]
    [UnitCategory("Control")]
    public sealed class InvokeUnit : ClassMemberUnit
    {
        [Serialize]
        public Method method;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput enter;

        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput exit;

        [DoNotSerialize]
        public List<ValueInput> parameters = new List<ValueInput>();

        [DoNotSerialize][PortLabelHidden]
        public ValueOutput result;

        private object returnValue; 

        protected override void Definition()
        {
            base.Definition();

            parameters.Clear();

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

                var parameterList = new List<object>();

                for (int i = 0; i < parameters.Count; i++)
                {
                    parameterList.Add(flow.GetValue(parameters[i]));
                }

                method.nest.Invoke(_target, (obj) => { returnValue = obj; }, parameterList.ToArray());
                return exit;
            });

            exit = ControlOutput("exit"); 
             
            if (method != null)   
            {
                if (method.nest.returnType != typeof(Void)) result = ValueOutput(method.nest.returnType, "result", (flow)=> { return returnValue; });

                var _parameters = method?.nest?.macro?.entry?.parameters;
                var keys = _parameters?.KeysToArray();

                for (int i = 0; i < keys?.Length; i++)
                {
                    parameters.Add(ValueInput(_parameters[keys[i]], keys[i])); 
                }

                if (method.nest?.macro?.entry != null)
                {
                    if (!method.nest.macro.entry.invokes.Contains(this)) method.nest.macro.entry.invokes.Add(this);
                }
            }
        }
    }
}