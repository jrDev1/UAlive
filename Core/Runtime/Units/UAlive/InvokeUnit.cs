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
        [DoNotSerialize]
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

            if (id != 0) method = FindWithID(id);

            if (method != null)
            {
                if (method.macro.entry == null)
                {
                    Debug.Log(method.macro.graph.units.Count);
                    foreach (IUnit unit in method.macro.graph.units)
                    {
                        Debug.Log(unit);
                    }
                    //method.macro.entry = method.macro.graph.units.Single((unit) => { return unit.GetType() == typeof(EntryUnit); }) as EntryUnit;
                }   

                if (method.macro != null && method.macro.entry != null)
                {
                    if (IsValidReturnType())
                    { 
                        result = ValueOutput(method.returnType, "result", (flow) => { return returnValue; });
                    } 

                    if (method.macro.entry.parameters.Count > 0)
                    {
                        var keys = method.macro.entry.parameters.KeysToArray(); 

                        for (int i = 0; i < keys.Length; i++)
                        { 
                            parameters.Add(ValueInput(method.macro.entry.parameters[keys[i]], keys[i]));
                        } 
                    }
                      
                    if (IsValidReturnType()) Requirement(target, result);
                }

                Requirement(target, enter);
                for (int i = 0; i < parameters.Count; i++)
                {
                    Requirement(parameters[i], enter);
                }
                Succession(enter, exit); 
            }
        }
        
        private bool IsValidReturnType()
        {
            return method.returnType != null && method.returnType != typeof(void) && method.returnType != typeof(Void);
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

        private Method FindWithID(int id)
        {
            if (macro.methods.custom.Any((v) => { return v.id == id; }))
            {
                return macro.methods.custom.Single((v) => { return v.id == id; });
            }

            return null;
        }
    }
}