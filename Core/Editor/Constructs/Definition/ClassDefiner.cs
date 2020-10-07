using Bolt;
using System;
using System.Reflection;

namespace Lasm.UAlive
{
    [Definer(typeof(CustomClass))]
    public sealed class ClassDefiner : Definer<CustomClass>
    {
        public Action onCreate { get; set; }

        public override bool changed => decorated.methods.Definer().changed;

        public override void Define()
        {
            BeforeDefine();
            Definition();
            AfterDefine();
            Refresh();
        }

        public override void Undefine()
        {
            decorated.methods.Definer().Undefine();
        }

        private void BeforeDefine()
        {
            decorated.methods.overrides.Clear();
        }

        private void AfterDefine()
        {
            decorated.methods.Definer().Undefine();
        }

        private void Definition()
        {
            // Define all overrides based on data from the reflected Method Info.
            foreach (MethodInfo method in decorated.inheritance.type.GetMethods(BindingFlags.Instance | BindingFlags.Static |
                                               BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)) 
            {
                if (method.Overridable())
                {
                    var _method = decorated.methods.Definer().SetMethod(decorated, MethodDeclaration.FromReflected(method));
                    if (method.IsAbstract) _method.entry.declaration.isOverridden = true;
                }
            }

            // Ensures all the variables of this class have notified observers that they have changed.
            // Generally we end up invoking the Define behaviour on all units to ensure the data is shown.
            for (int i = 0; i < decorated.variables.variables.Count; i++)
            {
                decorated.variables.variables[i].declaration.Class = decorated;
                decorated.variables.variables[i].declaration.guid = decorated.variables.variables[i].GetGUID();
                decorated.variables.variables[i].declaration.classGUID = decorated.GetGUID();
                decorated.variables.variables[i].declaration.Changed();
            }

            // Add all special methods for different UnityEngine.Object types that have special "Magic" methods. Aka Messages.
            if (MagicMethods.TryAddMonoBehaviour(decorated.methods, decorated, decorated.inheritance)) return;
            if (MagicMethods.TryAddScriptableObject(decorated.methods, decorated, decorated.inheritance)) return;
            if (MagicMethods.TryAddEditorWindow(decorated.methods, decorated, decorated.inheritance)) return;
        }

        public override void Refresh()
        {
            if (changed)
            {
                decorated.methods.Definer().Refresh();
                Refresh();
            }
        }
    }
}