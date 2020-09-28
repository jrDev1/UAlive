using Bolt;
using Ludiq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    [Widget(typeof(GetClassVariableUnit))]
    public class GetClassVariableUnitWidget : UnitWidget<GetClassVariableUnit>
    {
        public GetClassVariableUnitWidget(FlowCanvas canvas, GetClassVariableUnit unit) : base(canvas, unit)
        {
        }
    }
}
