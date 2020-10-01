using Bolt;
using Ludiq;
using System;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    [Widget(typeof(InvokeUnit))]
    public class InvokeUnitWidget : UnitWidget<InvokeUnit>
    {
        public InvokeUnitWidget(FlowCanvas canvas, InvokeUnit unit) : base(canvas, unit)
        {
        }

        protected override void OnDoubleClick()
        {
            base.OnDoubleClick();

            if (unit.method != null)
            {
                if (GraphWindow.active != null)
                {
                    GraphWindow.OpenActive(GraphReference.New(unit.method, true));
                }
            }
        }
    } 
}
