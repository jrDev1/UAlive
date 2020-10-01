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
            if (unit.graph.zoom == 1)
            {
                if (unit.method != null)
                {
                    var childReference = window.reference.ChildReference(unit, false);
                    if (childReference != null)
                    {
                        if (e.ctrlOrCmd)
                        {
                            GraphWindow.OpenTab(childReference);
                        }
                        else
                        {
                            window.reference = childReference;
                        }
                    }
                }
            }
            else
            {
                base.OnDoubleClick();
            }
        }

        protected override IEnumerable<DropdownOption> contextOptions
        {
            get
            {
                var childReference = reference.ChildReference(unit, false);
                yield return new DropdownOption((Action)(() => window.reference = childReference), "Open");
                yield return new DropdownOption((Action)(() => GraphWindow.OpenTab(GraphReference.New(unit.method, true))), "Open in new window");

                foreach (var baseOption in base.contextOptions)
                {
                    yield return baseOption;
                }
            }
        }
    } 
}
