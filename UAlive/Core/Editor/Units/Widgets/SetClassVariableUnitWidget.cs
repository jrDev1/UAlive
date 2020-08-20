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
        private bool onChangedSet;
        private Variable previous;

        public SetClassVariableUnitWidget(FlowCanvas canvas, SetClassVariableUnit unit) : base(canvas, unit)
        {
        }

        protected override bool showHeaderAddon => true;
        protected override float GetHeaderAddonHeight(float width)
        {
            return 20;
        }

        protected override NodeColorMix color => NodeColorMix.TealReadable;

        public override bool foregroundRequiresInput => true;

        protected override void DrawHeaderAddon()
        {
            var buttonText = "(None Selected)";
            if (unit.variable != null)
            {
                buttonText = unit.macro.title + "." + unit.variable.name;
            }

            if (GUI.Button(position.Add().X(42).Add().Y(23).Set().Height(20).Subtract().Width(51), buttonText))
            {
                var classes = HUMAssets.Find().Assets().OfType<ClassMacro>();

                GenericMenu menu = new GenericMenu();

                for (int i = 0; i < classes.Count; i++)
                {
                    for (int j = 0; j < classes[i].variables.variables.Count; j++)
                    {
                        menu.AddItem(new GUIContent(classes[i].title + "/" + classes[i].variables.variables[j].name), false, (data) => 
                        {
                            var tuple = (ValueTuple<ClassMacro, Variable>)data;
                            unit.macro = tuple.Item1;
                            unit.memberName = tuple.Item2.name;
                            previous = unit.variable;
                            unit.variable = tuple.Item2;
                            unit.variable.DefineGet();
                            unit.variable.DefineSet();
                            unit.Define();

                        }, (classes[i], classes[i].variables.variables[j]));
                    }
                }

                menu.ShowAsContext();
            }
        }
    }
}
