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

        [Serialize]
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

        /// <summary>
        /// Attempts to create a new method if it does not exist. If it does, it ensures the parameters are the same.
        /// </summary>
        public Method SetMethod(CustomClass instance, MethodDeclaration declaration)
        {
            Method _method = null;

            // If this is an override method we will attempt to create and define the method.
            if (instance != null)
            {
                _method = overrides.Define(declaration.name, ref _defineAdded,
                (method) =>
                {
                    var newMethod = Method.Create(instance);
                    return newMethod;
                }, null);

                if (_method != null)
                {
                    _method.entry.declaration.Copy(declaration);
                    _method.name = declaration.name; 
                    _method.entry.Define();
                    _method.entry.DefineReturns();
                }
            }

            return _method;
        }
    }
}