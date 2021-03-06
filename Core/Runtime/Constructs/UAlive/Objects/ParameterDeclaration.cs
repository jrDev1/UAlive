﻿using Ludiq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    [Serializable]
    [Inspectable]
    [InspectorLabel(null)]
    public sealed class ParameterDeclaration : ICopy<ParameterDeclaration>
    {
        [Inspectable]
        public string name = "Parameter" + new object().GetHashCode().ToString().Replace("-", string.Empty);
        
        public Type _type = typeof(object);
        [Inspectable]
        public Type type
        {
            get { return _type; }
            set
            {
                _type.Changed(value, (val) => { _type = value; changed(); });
            }
        }

        public event Action changed = ()=> { };

        public ParameterDeclaration(string name, Type type)
        {
            this.name = name;
            this.type = type;
        }

        public ParameterDeclaration() { }

        public static ParameterDeclaration WithName(ParameterDeclaration[] declarations, string name)
        {
            for (int i = 0; i < declarations.Length; i++)
            {
                if (declarations[i].name == name) return declarations[i];
            }

            return null;
        }

        public static ParameterDeclaration[] WithType(ParameterDeclaration[] declarations, Type type)
        {
            List<ParameterDeclaration> results = new List<ParameterDeclaration>();

            for (int i = 0; i < declarations.Length; i++)
            {
                if (declarations[i].type == type) results.Add(declarations[i]);
            }

            return results.ToArray();
        }

        public static ParameterDeclaration[] WithType<T>(ParameterDeclaration[] declarations, Type type)
        {
            List<ParameterDeclaration> results = new List<ParameterDeclaration>();

            for (int i = 0; i < declarations.Length; i++)
            {
                if (declarations[i].type == typeof(T)) results.Add(declarations[i]);
            }

            return results.ToArray();
        }

        public void Copy(ParameterDeclaration other)
        {
            type = other._type;
            name = other.name;
            changed = other.changed;
        }
    }
}