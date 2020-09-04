using Bolt;
using Ludiq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    [Serializable]
    public sealed class CustomClass : CustomType, IRefreshable, IDefinable
    {
        #region Declaration

        public override string GetDefaultName()
        {
            return "Custom Class " + this.GetInstanceID().ToString().Replace("-", string.Empty);
        }

        [Serialize]
        public Inheritance inheritance { get; set; } = new Inheritance();

        #endregion

        #region Members
        [Serialize]
        public Methods methods = new Methods();

        [Serialize]
        public Variables variables = new Variables();
        #endregion

        #region Definition

        public event Action definitionChanged = () => { };
        public event Action refreshed = () => { };
        public Action onCreate { get; set; }

        public bool changed => methods.changed;

        private bool _defineAdded;
        public bool defineAdded { get => _defineAdded; set => _defineAdded = value; }

        private bool _defineRemoved;
        public bool defineRemoved { get => _defineRemoved; set => _defineRemoved = value; }

        public void Define()
        {
            BeforeDefine();
            Definition();
            AfterDefine();
            Refresh();
        }

        public void Undefine()
        {
            methods.Undefine();
        }

        private void BeforeDefine()
        {
            methods.overrides.Clear();
        }

        private void AfterDefine()
        {
            methods.Undefine();
        }

        private void Definition()
        {
            // Define all overrides based on data from the reflected Method Info.
            foreach (MethodInfo method in inheritance.type.GetMethods())
            {
                if (method.Overridable())
                {
                    var _method = methods.SetMethod(this, MethodDeclaration.FromReflected(method));
                    if (method.IsAbstract) _method.entry.declaration.isOverridden = true;
                }
            }

            // Add all special methods for different UnityEngine.Object types that have special "Magic" methods. Aka Messages.
            if (MagicMethods.TryAddMonoBehaviour(methods, this, inheritance)) return;
            if (MagicMethods.TryAddScriptableObject(methods, this, inheritance)) return;
            if (MagicMethods.TryAddEditorWindow(methods, this, inheritance)) return;

            // Ensures all the variables of this class have notified observers that they have changed.
            // Generally we end up invoking the Define behaviour on all units to ensure the data is shown.
            for (int i = 0; i < variables.variables.Count; i++)
            {
                variables.variables[i].declaration.Changed();
            }
        }

        public void Refresh()
        {
            if (changed)
            {
                methods.Refresh();
                refreshed();
            }
        }

        #endregion

#if UNITY_EDITOR
        public EditorClassData editorData = new EditorClassData();
#endif

        #region Messages
        
        private void OnDisable()
        {
            DeserializationRoutine.Disable();
        }
        #endregion

    }
}
