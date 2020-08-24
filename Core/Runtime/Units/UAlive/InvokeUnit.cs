using Bolt;
using Lasm.OdinSerializer;
using Ludiq;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [DoNotSerialize]
        [PortLabelHidden]
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

                method.Invoke(_target, (obj) => { returnValue = obj; }, parameterList.ToArray());
                return exit;
            });

            exit = ControlOutput("exit"); 

            if (method != null)
            {
                id = method.id;
                 
                method = macro.methods.custom.Single((meth) => { return meth.id == id; });
                
                if (method.macro != null && method.macro.entry != null)
                {
                    if (method.macro.entry.returnType != typeof(Lasm.UAlive.Void) && method.macro.entry.returnType != typeof(void) && method.macro.entry.returnType != null)
                    {
                        result = ValueOutput(method.macro.entry.returnType, "result", (flow) => { return returnValue; });
                    }

                    var _parameters = method.macro.entry.parameters;

                    if (_parameters.Count > 0)
                    {
                        var keys = _parameters.KeysToArray(); 

                        for (int i = 0; i < keys.Length; i++)
                        {
                            parameters.Add(ValueInput(_parameters[keys[i]], keys[i]));
                        }
                    }

                    if (method.returnType != null && method.returnType != typeof(void) && method.returnType != typeof(Void)) Requirement(target, result);
                }

                Requirement(target, enter);
                for (int i = 0; i < parameters.Count; i++)
                {
                    Requirement(parameters[i], enter);
                }
                Succession(enter, exit); 
            }
        }
         
        protected override void AfterDefine()
        {
            if (method?.macro?.entry != null)
            {
                method.macro.entry.onChanged += Define;
            }
        }

        protected override void BeforeUndefine()
        {  
            if (method?.macro?.entry != null)
            {
                method.macro.entry.onChanged -= Define;
            }
        }
    }
}