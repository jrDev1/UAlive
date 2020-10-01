using Bolt;
using Ludiq;
using System;
using System.Collections.Generic;
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
                window.reference = GraphReference.New(unit.method, true);
            }
        }

        protected override IEnumerable<DropdownOption> contextOptions
        {
            get
            {
                var reference = GraphReference.New(unit.method, true);
                yield return new DropdownOption((Action)(() => window.reference = reference), "Open");
                yield return new DropdownOption((Action)(() => GraphWindow.OpenTab(reference)), "Open in new window");

                foreach (var baseOption in base.contextOptions)
                {
                    yield return baseOption;
                }
            }
        }
    } 
}
