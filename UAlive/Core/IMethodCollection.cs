using Ludiq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lasm.UAlive
{
    public interface IMethodCollection
    {
        int Count { get; }
        void Define(ClassMacro instance, string title);
        Dictionary<string, Method> overrides { get; }
        List<Method> custom { get; }
        Method New(ClassMacro owner, string name, AccessModifier scope, MethodModifier modifier, Type returnType, ParameterDeclaration[] parameters, bool isMagic = false);
        void Refresh();
        bool CanAdd();
    }
}