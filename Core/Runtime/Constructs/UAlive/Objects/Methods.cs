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
    public sealed class Methods : IRefreshable, IDefinable
    {
        #region Definition

        [OdinSerialize]
        public MethodDictionary overrides = new MethodDictionary();
        
        [OdinSerialize]
        public List<Method> custom = new List<Method>();

        public event Action definitionChanged = () => { };
        public event Action refreshed = () => { };

        private void DefineMethod(string name, Method method)
        {
            method.name = name;
            method.entry.Define();
        }

        public void Define()
        {
            Refresh();
        }

        public void Undefine()
        {
            overrides.Undefine(ref _defineRemoved, (method) =>
            {
                UnityEngine.Object.DestroyImmediate(method, true);
            });

            if (defineRemoved)
            {
                definitionChanged();
            }
        }

        private bool _defineAdded;
        public bool defineAdded { get => _defineAdded; private set => _defineAdded = value; }

        private bool _defineRemoved;
        public bool defineRemoved { get => _defineRemoved; private set => _defineRemoved = value; }

        public bool changed => defineAdded || defineRemoved;

#if UNITY_EDITOR
        private CustomType GetRootAsset(CustomClass instance)
        {
            return AssetDatabase.LoadAssetAtPath<CustomType>(AssetDatabase.GUIDToAssetPath(HUMAssets.GetGUID(instance)));
        }
#endif

        public void Refresh()
        {
            defineAdded = false;
            defineRemoved = false;
            refreshed();
        }

        #endregion

        private bool IsOverridable(MethodDeclaration declaration)
        {
            return declaration.modifier == MethodModifier.Override || 
                declaration.isMagic;
        }

        private Method Create(MethodDeclaration declaration, CustomType asset)
        {
            var _method = Method.Create(asset);
            _method.name = declaration.name;
            SetParameters(_method, declaration.parameters);
            DefineMethod(declaration.name, _method);
            AssetDatabase.AddObjectToAsset(_method, asset);
            return _method;
        }

        public Method TryCreateMethod(CustomClass instance, MethodDeclaration declaration)
        {
            var asset = GetRootAsset(instance);
            Method _method = null;

            if (IsOverridable(declaration))
            {
                if (asset != null)
                {
                    _method = overrides.Define(declaration.name, ref _defineAdded,
                    (method) =>
                    {
                        return Create(declaration, asset);
                    },
                    (method) =>
                    {
                        EnsureParametersMatch(declaration.name, declaration.parameters);
                        SetOverrideMethod(declaration.name);
                    });
                }
                 
                if (_method != null)
                {
                    var overridden = _method.entry.declaration.isOverridden;
                    var type = _method.entry.declaration.type;
                    _method.entry.declaration.Copy(declaration);
                    _method.entry.declaration.isOverridden = overridden;
                    _method.entry.declaration.hasOptionalOverride = true;

                    if (type != declaration.type)
                    {
                        _method.entry.declaration.type = declaration.type;
                        _method.entry.DefineReturns();
                    }

                    _method.hideFlags = HideFlags.HideInHierarchy;
                }
            }
            else
            {
                if (_method != null)
                {
                    _method.entry.declaration.scope = declaration.scope;
                    _method.entry.declaration.modifier = declaration.modifier;
                    _method.name = declaration.name;
                    _method.entry.declaration.isMagic = false;
                    _method.entry.declaration.isOverridden = false;
                    _method.entry.declaration.hasOptionalOverride = false;
                    _method.hideFlags = HideFlags.HideInHierarchy;
                    _method.entry.declaration.type = declaration.type;
                    _method.entry.Define(); 
                }
            }

            return _method;
        }

        private void EnsureParametersMatch(string name, ParameterDeclaration[] parameters)
        {
            var shouldDefine = false;
            var method = overrides[name];

            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = ParameterDeclaration.WithName(method.entry.declaration.parameters, parameters[i].name);

                if (parameter == null)
                {
                    shouldDefine = true;
                    break;
                }

                if (parameter.type != parameters[i].type)
                {
                    shouldDefine = true;
                    break;
                }
            }

            if (method.entry.declaration.parameters.Length != parameters.Length)
            {
                shouldDefine = true;
            }

            if (shouldDefine)
            {
                SetParameters(method, parameters);
                method.entry.Define();
            }
        }

        private void SetOverrideMethod(string name)
        {
            var method = overrides[name];
            overrides.Add(name, method);
        }

        private void SetParameters(Method method, ParameterDeclaration[] parameters)
        {
            var tempList = new List<ParameterDeclaration>();
            for (int i = 0; i < parameters.Length; i++)
            {
                tempList.Add(new ParameterDeclaration(parameters[i].name, parameters[i].type));
            }

            method.entry.declaration.parameters = tempList.ToArray();
        }
    }
}