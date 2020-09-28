using Bolt;
using Ludiq;
using System;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    [Widget(typeof(SetClassVariableUnit))]
    public class SetClassVariableUnitWidget : UnitWidget<SetClassVariableUnit>
    {
        public SetClassVariableUnitWidget(FlowCanvas canvas, SetClassVariableUnit unit) : base(canvas, unit)
        {
        }
    }
}
