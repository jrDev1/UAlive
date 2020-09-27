using System.Collections;
using System.Collections.Generic;
using Ludiq;
using Bolt;
using System.Linq;
using UnityEngine;

namespace Lasm.UAlive
{
    [Widget(typeof(EntryUnit))]
    public class EntryUnitWidget : UnitWidget<EntryUnit>
    {
        private int count;

        public EntryUnitWidget(FlowCanvas canvas, EntryUnit unit) : base(canvas, unit)
        {

        }

        private bool isDeleting;
        public override bool canDelete => isDeleting;
        
        protected override NodeColorMix color => new NodeColorMix(NodeColor.Blue) { red = 0.3f };
        public override void HandleInput()
        {
            var entries = unit.graph.units.OfType<EntryUnit>();
            var type = reference?.macro?.GetType();
            var isMethod = type == typeof(Method);
            if (entries.Count() > 1 || !isMethod)
            {
                var list = entries.ToList();
                if (unit != list[0] || !isMethod)
                {
                    isDeleting = true;
                    selection.Clear();
                    selection.Add(isMethod ? list[1] : list[0]);
                    Delete();
                    if (isMethod)
                    {
                        Debug.LogWarning("You cannot have more then one EntryUnit in a Method. Auto deleting.");
                    }
                    else
                    {
                        Debug.LogWarning("You cannot have an EntryUnit outside of a Method. Auto deleting.");
                    }
                }
                else
                {
                    isDeleting = false;
                }

                GraphWindow.active.Repaint();
            }

            base.HandleInput();
        }
    }
}