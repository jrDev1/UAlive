using Lasm.OdinSerializer;
using Ludiq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    [Serializable]
    public sealed class MethodCollection
    {
        [Serialize]
        public Dictionary<string, Method> previousOverrides = new Dictionary<string, Method>();
        [Serialize]
        public Dictionary<string, Method> overrides = new Dictionary<string, Method>();

        public List<Method> custom = new List<Method>(); 
         
#if UNITY_EDITOR
        public bool addedMethod = false;
        public bool removedMethod = false;
#endif

        private Method Add(Method method)
        {
            previousOverrides.Add(method.name, method);
            overrides.Add(method.name, method);
            return method;
        }

#if UNITY_EDITOR
        public bool CanAdd()
        {
            return addedMethod || removedMethod;
        }

        private TypeMacro GetRootAsset(ClassMacro instance)
        {
            return AssetDatabase.LoadAssetAtPath<TypeMacro>(AssetDatabase.GUIDToAssetPath(HUMAssets.GetGUID(instance)));
        }

        public void Refresh()
        {
            addedMethod = false;
            removedMethod = false;
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
#endif
        public void Define(ClassMacro instance, string title)
        {
            foreach (Method method in custom)
            {
                New(instance, method.name, AccessModifier.Public, MethodModifier.None, method.returnType, new ParameterDeclaration[] { });
            }
        }

        public Method New(ClassMacro instance, string name, AccessModifier scope, MethodModifier modifier, Type returnType, ParameterDeclaration[] parameters, bool isMagic = false)
        {
            var asset = GetRootAsset(instance);
            Method _method = null;

            if (modifier == MethodModifier.Override || modifier == MethodModifier.None && isMagic)
            {
                return Override();
            }

            return Custom();

            Method Override()
            {
                asset.Is().NotNull(() =>
                {
                    _method = overrides.Define(previousOverrides, name,
                    (method) =>
                    {
                        _method = CreateNest(out addedMethod);
                        _method.name = name;
                        SetNestEntryParameters(_method, parameters);
                        DefineNestMacro(name, _method);
                        Add(_method);
                        AssetDatabase.AddObjectToAsset(_method.macro, asset);
                        addedMethod = true;
                        return _method;
                    },
                    (method) =>
                    {
                        EnsureParametersMatch(name, parameters);
                        addedMethod = true;
                        SetOverrideMethod(name);
                        _method = method;
                    });
                });

                if (_method != null)
                {
                    _method.scope = scope;
                    _method.modifier = modifier;
                    _method.returnType = returnType;
                    _method.isSpecial = isMagic;
                    _method.hasOptionalOverride = true;

                    if (_method.macro.entry.returnType != returnType)
                    {
                        _method.macro.entry.returnType = returnType;
                        _method.macro.entry.DefineReturns();
                    }
                }

                return _method;
            }

            Method Custom()
            {
                if (_method != null)
                {
                    _method.scope = scope;
                    _method.modifier = modifier;
                    _method.returnType = returnType;
                    _method.name = name;
                    _method.isSpecial = false;

                    if (_method.macro.entry.returnType != returnType)
                    {
                        _method.macro.entry.returnType = returnType;
                    }

                    _method.macro.entry.Define();
                }

                return _method;
            }

            Method CreateNest(out bool nestAdded)
            {
                var nest = new Method();
                nest.Initialize();
                nestAdded = true;
                return nest;
            }
        }

        private void EnsureParametersMatch(string name, ParameterDeclaration[] parameters)
        {
            var shouldDefine = false;
            var method = previousOverrides[name];

            for (int i = 0; i < parameters.Length; i++)
            {
                if (!method.macro.entry.parameters.ContainsKey(parameters[i].name))
                {
                    shouldDefine = true;
                    break;
                }

                if (method.macro.entry.parameters[parameters[i].name] != parameters[i].type)
                {
                    shouldDefine = true;
                    break;
                }
            }

            if (method.macro.entry.parameters.Count != parameters.Length)
            {
                shouldDefine = true;
            }

            if (shouldDefine)
            {
                method.macro.entry.parameters.Clear();
                for (int i = 0; i < parameters.Length; i++)
                {
                    method.macro.entry.parameters.Add(parameters[i].name, parameters[i].type);
                }

                method.macro.entry.Define();
            }
        }

        private void SetOverrideMethod(string name)
        {
            var method = previousOverrides[name];
            overrides.Add(name, method);
            method.Initialize();
        }

        private void SetNestEntryParameters(Method nest, ParameterDeclaration[] parameters)
        {
            for (int i = 0; i < parameters?.Length; i++)
            {
                nest.macro.entry.parameters.Add(parameters[i].name, parameters[i].type);
            }
        }

        private void DefineNestMacro(string name, Method nest)
        {
            nest.macro.name = name;
            nest.macro.entry.Define();
        }

        public void RemoveUnusedMethods()
        {
            var removeAmount = 10;

            for (int i = 0; i < removeAmount; i++)
            {
                if (i > 0 && !removedMethod) break;

                overrides.Undefine(previousOverrides, (method) =>
                {
                    UnityEngine.Object.DestroyImmediate(method.macro, true);
                    removedMethod = true;
                });
            }

            if (removedMethod)
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}