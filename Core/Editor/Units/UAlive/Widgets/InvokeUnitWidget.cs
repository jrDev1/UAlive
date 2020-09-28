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
    } 
}
