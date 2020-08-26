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
    public sealed class CustomClass : CustomType, IClassDeclaration, IRefreshable, IDefinable
    {
        #region Declaration

        public override string GetDefaultName()
        {
            return "New Custom Class";
        }

        [Serialize]
        public Inheritance inheritance = new Inheritance();

        #endregion

        #region Members
        [Serialize]
        public Methods methods = new Methods();

        [Serialize]
        public Variables variables = new Variables();
        #endregion

#if UNITY_EDITOR
        public EditorClassData editorData;
#endif

        public void Define()
        {
            BeforeDefine();
            Definition();
            AfterDefine();
            Refresh();
        }

        private void BeforeDefine()
        {
            methods.overrides.Clear();
        }

        private void AfterDefine()
        {
            methods.RemoveUnusedMethods();
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
                    var meth = methods.New(this, method.Name, method.GetScope(), modifier, method.ReturnType, methodParams.ToArray());
                    if (method.IsVirtual) meth.hasOptionalOverride = true;
                }
            }

            UnityMethods();
        }

        public void Refresh()
        {
            if (methods.CanAdd())
            {
                methods.Refresh();
            }
        }

        private void UnityMethods()
        {
            if (MagicMethods.TryAddMonoBehaviour(methods, this, inheritance)) return;
            if (MagicMethods.TryAddScriptableObject(methods, this, inheritance)) return;
            if (MagicMethods.TryAddEditorWindow(methods, this, inheritance)) return;
        }

    }
}
