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
        [Inspectable]
        public bool chain;

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

        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput chainTarget;

        private object returnValue;

        protected override void Definition()
        {
            base.Definition();

            parameters.Clear();

            if (chain) chainTarget = ValueOutput<IUAClass>("chain", (flow) => { return flow.GetValue<IUAClass>(target); });

            if (method != null)
            {
                if (method != null && method.entry != null)
                {
                    if (IsValidReturnType())
                    {
                        result = ValueOutput(method.entry.declaration.type, "result", (flow) =>
                        {
                            IUAClass _target = null;
                            return Invoke(ref _target, flow);
                        });
                    }

                    if (method.entry.declaration.parameters?.Length > 0)
                    {
                        for (int i = 0; i < method.entry.declaration.parameters.Length; i++)
                        {
                            parameters.Add(ValueInput(method.entry.declaration.parameters[i].type, method.entry.declaration.parameters[i].name));
                        }
                    }

                    if (IsValidReturnType()) Requirement(target, result);
                }
            }

            if (method != null && method.entry != null && !method.entry.declaration.pure)
            {
                enter = ControlInput("enter", (flow) =>
                {
                    IUAClass _target = null; 
                    Invoke(ref _target, flow);
                    return exit;
                }); 

                exit = ControlOutput("exit");

                for (int i = 0; i < parameters.Count; i++)
                {
                    Requirement(parameters[i], enter);
                }

                Requirement(target, enter);
                Succession(enter, exit);
            }
        }

        public object Invoke(ref IUAClass target, Flow flow)
        {
            if (this.target.hasValidConnection)
            {
                target = flow.GetValue<IUAClass>(this.target);
            }
            else
            {
                target = (IUAClass)flow.variables.Get("#secret_uaclass_instance");
            }

            var parameterList = new List<object>();

            for (int i = 0; i < parameters.Count; i++)
            {
                parameterList.Add(flow.GetValue(parameters[i]));
            }

            method.Invoke(target, (obj) => { returnValue = obj; }, parameterList.ToArray());

            return returnValue;
        }

        private bool IsValidReturnType()
        {
            return method.entry.declaration.type != null && method.entry.declaration.type != typeof(void) && method.entry.declaration.type != typeof(Void);
        }

        protected override void AfterDefine()
        {
            if (method != null && method.entry != null)
            {
                method.entry.onChanged += Define;
            }
        } 

        protected override void BeforeUndefine()
        {
            if (method != null && method.entry != null)
            {
                method.entry.onChanged -= Define;
            }
        }

        public Method FindWithGUID(string guid)
        {
            if (Class.methods.custom.Any((method) => { return method.entry.declaration.guid == guid && !string.IsNullOrEmpty(guid); }))
            {
                return Class.methods.custom.Single((method) => { return method.entry.declaration.guid == guid && !string.IsNullOrEmpty(guid); });
            }

            return null;
        }
    }
}