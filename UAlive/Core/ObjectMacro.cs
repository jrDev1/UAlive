using Ludiq;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    public abstract class ObjectMacro : TypeMacro
    {
        [Serialize]
        public Dictionary<string, FlowNest> lastDefinedOverrideMethods = new Dictionary<string, FlowNest>();
        [Serialize]
        public Dictionary<string, FlowNest> overrideMethods = new Dictionary<string, FlowNest>();
        [Serialize]
        public List<Method> methods = new List<Method>();
        [Inspectable]
        public Variables variables = new Variables();

#if UNITY_EDITOR
        [SerializeField]
        protected bool firstTime = true;
        protected bool addedMethod = false;
        protected bool removedMethod = false;

        public bool customOpen;
        public bool overridesOpen;
        public bool customVariablesOpen;
        public bool customMethodsOpen;
        public bool methodOverridesOpen;
        public bool propertyOverridesOpen;
#endif

        protected override void Definition()
        {
            
        }

        protected override void RefreshOnChange()
        {
            base.RefreshOnChange();

            if (addedMethod || removedMethod)
            {
                addedMethod = false;
                removedMethod = false;
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private FlowNest CreateMethod(string name, FlowNest nest)
        {
            lastDefinedOverrideMethods.Add(name, nest);
            overrideMethods.Add(name, nest);
            return nest;
        }

        public FlowNest MethodOverride(string name, AccessModifier scope, MethodModifier modifier, Type returnType, string methodInfoName, (string name, Type type)[] parameters, bool isMagic = false)
        {
            var asset = GetRootAsset();
            FlowNest _method = null;

            asset.Is().NotNull(() =>
            {
                _method = overrideMethods.Define(lastDefinedOverrideMethods, name,
                (method) => 
                {
                    method = CreateNest(name, out addedMethod);
                    SetNestEntryParameters(method, parameters);
                    DefineNestMacro(name, method);
                    CreateMethod(name, method);
                    AssetDatabase.AddObjectToAsset(method.macro, asset);
                    addedMethod = true;
                    return method;
                },
                (method) => 
                {
                    EnsureParametersMatch(name, parameters);
                    addedMethod = true;
                    SetOverrideMethod(name);
                });
            });

            if (_method != null)
            {
                _method.scope = scope;
                _method.modifier = modifier;
                _method.returnType = returnType;
                _method.name = methodInfoName;
                _method.isSpecial = isMagic;

                if (_method.macro.entry.returnType != returnType)
                {
                    _method.macro.entry.returnType = returnType;
                    _method.macro.entry.DefineReturns();
                }
            }

            return _method;
        }

        public FlowNest CustomMethod(FlowNest _method, string name, string subtitle, AccessModifier scope, MethodModifier modifier, Type returnType, string methodInfoName, (string name, Type type)[] parameters)
        {
            var asset = GetRootAsset();

            if (_method != null)
            {
                _method.scope = scope;
                _method.modifier = modifier;
                _method.returnType = returnType;
                _method.name = methodInfoName;
                _method.isSpecial = false;

                if (_method.macro.entry.returnType != returnType)
                {
                    _method.macro.entry.returnType = returnType;
                }

                _method.macro.entry.Define();
            }

            return _method;
        }

        private void EnsureParametersMatch(string name, (string name, Type type)[] parameters)
        {
            var shouldDefine = false;

            for (int i = 0; i < parameters.Length; i++)
            {
                if (!lastDefinedOverrideMethods[name].macro.entry.parameters.ContainsKey(parameters[i].name))
                {
                    shouldDefine = true;
                    break;
                }

                if (lastDefinedOverrideMethods[name].macro.entry.parameters[parameters[i].name] != parameters[i].type)
                {
                    shouldDefine = true;
                    break;
                }
            }

            if (lastDefinedOverrideMethods[name].macro.entry.parameters.Count != parameters.Length)
            {
                shouldDefine = true;
            }

            if (shouldDefine)
            {
                lastDefinedOverrideMethods[name].macro.entry.parameters.Clear();
                for (int i = 0; i < parameters.Length; i++)
                {
                    lastDefinedOverrideMethods[name].macro.entry.parameters.Add(parameters[i].name, parameters[i].type);
                }
                lastDefinedOverrideMethods[name].macro.entry.Define();
            }
        }

        private void SetOverrideMethod(string name)
        {
            overrideMethods.Add(name, lastDefinedOverrideMethods[name]);
            overrideMethods[name].Initialize();
        }

        protected void SetNestEntryParameters(FlowNest nest, (string name, Type type)[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                nest.macro.entry.parameters.Add(parameters[i].name, parameters[i].type);
            }
        }

        protected void DefineNestMacro(string name, FlowNest nest)
        {
            nest.macro.name = name;
            nest.macro.entry.Define();
        }

        protected FlowNest CreateNest(string name, out bool nestAdded)
        {
            var nest = new FlowNest();
            nest.Initialize();
            nestAdded = true;
            return nest;
        }
    }
}