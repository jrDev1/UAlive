using Lasm.OdinSerializer;
using Ludiq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lasm.UAlive
{
    [Serializable]
    public sealed class Variable : LudiqScriptableObject, IUnityInitializable
    {
        [SerializeReference] 
        public VariableDeclaration declaration = new VariableDeclaration();

        #region Initialization
         
        [Serialize]
        private bool _isInitialized;
        public bool isInitialized { get => _isInitialized; private set => _isInitialized = value; }

        public void Initialize(CustomType owner, object data = null)
        {
            getter = Method.Create(owner);
            setter = Method.Create(owner);
            declaration.guid = this.GetGUID();
            declaration.classGUID = owner.GetGUID();
            getter.name = string.Empty;
            setter.name = string.Empty;
            hideFlags = HideFlags.HideInHierarchy;
            getter.hideFlags = HideFlags.HideInHierarchy;
            setter.hideFlags = HideFlags.HideInHierarchy;
            isInitialized = true;
        } 
          
        public static Variable Create(CustomType owner)
        {
            var variable = CreateInstance<Variable>();
            variable.Initialize(owner);
            return variable;
        }

        #endregion

        [Serialize] 
        public Method getter;

        [Serialize]
        public Method setter;
    }
} 