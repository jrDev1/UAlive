﻿using Bolt;
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
    [UnitCategory("Codebase")]
    public sealed class InvokeUnit : MethodUnit, IGraphParentElement
    {
        [Serialize]
        [Inspectable]
        public bool chain;

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

        public IGraph childGraph => method.graph;

        public bool isSerializationRoot => false;

        public UnityEngine.Object serializedObject => method;

        public InvokeUnit() : base() { }

        public InvokeUnit(CustomClass @class, Method method) : base (@class, method)
        {
        }

        protected override void Definition()
        {
            base.Definition();

            parameters.Clear();

            if (chain)
            {
                if (castedType == null)
                {
                    chainTarget = ValueOutput<IUAClass>("chain", (flow) => { return flow.GetValue<IUAClass>(target); });
                }
                else
                {
                    chainTarget = ValueOutput(castedType, "target");
                }
            }

            if (method != null)
            {
                if (method != null && method.entry != null)
                {
                    if (IsValidReturnType())
                    {
                        result = ValueOutput(method.entry.declaration.type, "result", (flow) =>
                        {
                            IUAClass _target = null;
                            return Invoke(ref _target, castedType, flow);
                        });
                    }

                    if (method.entry.declaration.parameters?.Length > 0)
                    {
                        for (int i = 0; i < method.entry.declaration.parameters.Length; i++)
                        {
                            var valInput = ValueInput(method.entry.declaration.parameters[i].type, method.entry.declaration.parameters[i].name);
                            valInput.SetDefaultValue(method.entry.declaration.parameters[i].type.Default());
                            parameters.Add(valInput);
                        }
                    }

                    if (IsValidReturnType()) Requirement(target, result);
                }
            }

            if (method != null && method.entry != null && (!method.entry.declaration.pure || method.entry.declaration.type == typeof(Lasm.UAlive.Void)))
            {
                enter = ControlInput("enter", (flow) =>
                {
                    IUAClass _target = null; 
                    Invoke(ref _target, castedType, flow);
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
            else
            {
                if (method != null && method.entry != null && method.entry.declaration != null && !method.entry.declaration.pure)
                {
                    enter = ControlInput("enter", (flow) =>
                    {
                        Debug.Log("Method selection is null on InvokeUnit.");
                        return null;
                    });

                    exit = ControlOutput("exit");
                }
            }
        }

        public object Invoke(ref IUAClass target, Type targetType, Flow flow)
        {
            target = flow.GetValue<IUAClass>(this.target);

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

        public IGraph DefaultGraph()
        {
            return new FlowGraph();
        }
    }
}