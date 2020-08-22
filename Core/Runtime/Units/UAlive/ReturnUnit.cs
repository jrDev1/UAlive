using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using Bolt;
using System;

namespace Lasm.UAlive
{
    [UnitTitle("Return")]
    [UnitCategory("Control")]
    [TypeIcon(typeof(Flow))]
    public sealed class ReturnUnit : Unit
    {
        [Serialize]
        public EntryUnit entry;
        [DoNotSerialize][PortLabelHidden]
        public ControlInput @return;
        [Serialize]
        public Type returnType = typeof(object);
        [DoNotSerialize][PortLabelHidden]
        public ValueInput value;

        protected override void Definition()
        {
            @return = ControlInput("return", Return);
            if (entry != null)
            {
                if (entry.returnType != null && entry.returnType != typeof(void) && entry.returnType != typeof(Lasm.UAlive.Void)) value = ValueInput(entry.returnType, "value");
            }
            else
            {
                value = ValueInput<object>("value");
            }
        }

        public ControlOutput Return(Flow flow)
        {
            if (entry?.macro != null)
            {
                entry.returnMethod(flow.GetValue<object>(value));
            }

            return null;
        }

        public override void AfterAdd()
        {
            base.AfterAdd();

            for (int i = 0; i < graph.units.Count; i++)
            {
                if (graph.units[i].GetType() == typeof(EntryUnit))
                {
                    entry = (EntryUnit)graph.units[i];
                    entry.AddReturn(this);
                    returnType = entry.returnType;
                    break; 
                }
            }
        }

        public override void BeforeRemove()
        {
            if (entry.returns != null && entry.returns.Contains(this)) entry.returns.Remove(this);
            base.BeforeRemove();
        }
    }
}