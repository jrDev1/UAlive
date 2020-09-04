using Lasm.OdinSerializer;
using Ludiq;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace Lasm.UAlive
{
    [Serializable]
    [Inspectable]
    public sealed class MethodDeclaration : ICopy<MethodDeclaration>
    {
        #region Editor
        public bool parametersOpen;
        #endregion

        [Serialize]  
        [Inspectable(order = 0)]
        public string name;

        [Serialize]
        public AccessModifier scope;

        [Serialize]
        public MethodModifier modifier;

        [Serialize]
        private Type _type = typeof(Lasm.UAlive.Void);
        [Inspectable(order = 2)]
        public Type type
        {
            get => _type;
            set
            {
                _type = value;
                changed();
            }
        }

        [Inspectable(order = 3)]
        public bool pure;

        [Inspectable(order = 4)]
        [InspectorWide]
        [InspectorLabel(null)]
        [Serialize]
        public ParameterDeclaration[] parameters;

        [Serialize] 
        public bool isMagic;

        [Serialize]
        public bool hasOptionalOverride => modifier == MethodModifier.Abstract 
            || (modifier == MethodModifier.Override && modifier != MethodModifier.Sealed) 
            || modifier == MethodModifier.Virtual || isMagic;

        [Serialize]
        public bool isOverridden;

        public bool isAbstract;

        public event Action changed = () => { };

        public void Changed()
        {
            changed();
        }
         
        public MethodDeclaration() { }

        public MethodDeclaration(string name, AccessModifier scope, MethodModifier modifier, Type type, ParameterDeclaration[] parameters, bool isMagic = false, bool isAbstract = false)
        {
            this.name = name;
            this.scope = scope;
            this.modifier = modifier;
            this.type = type;
            this.isMagic = isMagic;
            this.parameters = parameters;
            this.isAbstract = isAbstract;
        }

        public static MethodDeclaration FromReflected(MethodInfo methodInfo)
        {
            List<ParameterDeclaration> parameters = new List<ParameterDeclaration>();

            foreach (ParameterInfo parameter in methodInfo.GetParameters())
            {
                parameters.Add(new ParameterDeclaration(parameter.Name, parameter.ParameterType));
            }

            return new MethodDeclaration(methodInfo.Name, methodInfo.GetScope(), methodInfo.GetModifier(), methodInfo.ReturnType, parameters.ToArray(), isAbstract: methodInfo.IsAbstract);
        }

        #region Copy

        public void Copy(MethodDeclaration other)
        {
            name = other.name;
            scope = other.scope;
            modifier = other.modifier;
            type = other.type;
            isMagic = other.isMagic;
            pure = other.pure;
            isAbstract = other.isAbstract;
            changed = other.changed;

            var newParams = new List<ParameterDeclaration>();

            for (int i = 0; i < other.parameters.Length; i++)
            {
                newParams.Add(other.parameters[i]);
            }

            parameters = newParams.ToArray();
        }

        #endregion
    }
}