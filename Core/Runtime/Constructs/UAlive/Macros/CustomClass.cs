﻿using Bolt;
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
            return "New Custom Class";
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
            foreach (MethodInfo method in inheritance.type.GetMethods())
            {
                if (method.Overridable())
                {
                    List<ParameterDeclaration> methodParams = new List<ParameterDeclaration>();

                    foreach (ParameterInfo parameter in method.GetParameters())
                    {
                        methodParams.Add(new ParameterDeclaration(parameter.Name, parameter.ParameterType));
                    }

                    var modifier = method.GetModifier() == MethodModifier.Abstract || method.GetModifier() == MethodModifier.Virtual ? MethodModifier.Override : method.GetModifier();
                    var meth = methods.TryCreateMethod(this, new MethodDeclaration(method.Name, method.GetScope(), modifier, method.ReturnType, methodParams.ToArray()));
                    if (method.IsVirtual) meth.entry.declaration.hasOptionalOverride = true;
                }
            }

            UnityMethods();
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

        private void UnityMethods()
        {
            if (MagicMethods.TryAddMonoBehaviour(methods, this, inheritance)) return;
            if (MagicMethods.TryAddScriptableObject(methods, this, inheritance)) return;
            if (MagicMethods.TryAddEditorWindow(methods, this, inheritance)) return;
        }

        #endregion

    }
}
