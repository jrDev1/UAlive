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
        private bool onChangedSet;

        public InvokeUnitWidget(FlowCanvas canvas, InvokeUnit unit) : base(canvas, unit)
        {
        }

        protected override bool showHeaderAddon => true;

        protected override float GetHeaderAddonHeight(float width)
        {
            return 20;
        }

        protected override float GetHeaderAddonWidth()
        {
            return 120;
        }

        public override bool foregroundRequiresInput => true;

        protected override void DrawHeaderAddon()
        {
            var buttonText = "(None Selected)";
            if (unit.method != null)
            {
                buttonText = unit.macro.title + "." + unit.method.name;
            }

            if (GUI.Button(position.Add().X(42).Add().Y(23).Set().Height(20).Subtract().Width(51), buttonText))
            {
                var classes = HUMAssets.Find().Assets().OfType<ClassMacro>();

                GenericMenu menu = new GenericMenu();

                for (int i = 0; i < classes.Count; i++)
                {
                    for (int j = 0; j < classes[i].methods.Count; j++)
                    {
                        menu.AddItem(new GUIContent(classes[i].title + "/" + classes[i].methods[j].name), false, (data) =>
                        {
                            var tuple = (ValueTuple<ClassMacro, Method>)data;
                            unit.macro = tuple.Item1;
                            unit.memberName = tuple.Item2.name;
                            unit.method = tuple.Item2;
                            unit.Define();
                        }, (classes[i], classes[i].methods[j]));
                    }
                }

                menu.ShowAsContext();
            }
        }
    } 
}
