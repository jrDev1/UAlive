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
        private float buttonPadding => 8;
        private bool missingContent => unit.macro == null && unit.method == null;

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
            return Mathf.Clamp(GUI.skin.label.CalcSize(new GUIContent(unit.method == null ? "   (None Selected)   " : unit.macro?.title + "." + unit.method?.name)).x + buttonPadding, base.GetHeaderAddonWidth(), 400);
        }

        public override bool foregroundRequiresInput => true;

        protected override void DrawHeaderAddon()
        {
            var buttonText = "(None Selected)";
            if (unit.method != null)
            {
                buttonText = unit.macro.title + "." + unit.method.name;
            }

            if (GUI.Button(position.Add().X(42).Add().Y(23).Set().Height(20).Set().Width(missingContent ? 120 : GUI.skin.label.CalcSize(new GUIContent(unit.macro?.title + "." + unit.method?.name)).x + buttonPadding), buttonText))
            {
                var classes = HUMAssets.Find().Assets().OfType<ClassMacro>();

                GenericMenu menu = new GenericMenu();

                for (int i = 0; i < classes.Count; i++)
                {
                    for (int j = 0; j < classes[i].methods.custom.Count; j++)
                    {
                        menu.AddItem(new GUIContent(classes[i].title + "/" + classes[i].methods.custom[j].name), false, (data) =>
                        {
                            var tuple = (ValueTuple<ClassMacro, Method>)data;
                            unit.macro = tuple.Item1;
                            unit.memberName = tuple.Item2.name;
                            unit.method = tuple.Item2;
                            unit.Define();
                        }, (classes[i], classes[i].methods.custom[j]));
                    }
                }

                menu.ShowAsContext();
            }
        }
    } 
}
