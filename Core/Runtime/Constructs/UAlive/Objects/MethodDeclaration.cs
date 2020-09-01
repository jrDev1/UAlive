using Lasm.OdinSerializer;
using Ludiq;
using System;
using UnityEditor;

namespace Lasm.UAlive
{
    [Serializable]
    [Inspectable]
    public sealed class MethodDeclaration : ICopy<MethodDeclaration>
    {
        #region Assets

        [Serialize]
        public string guid;

        [Serialize]
        public string classGUID;

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
        public bool hasOptionalOverride;

        [Serialize]
        public bool isOverridden;
          
        public event Action changed = () => { };
         
        public MethodDeclaration() { }

        public MethodDeclaration(string name, AccessModifier scope, MethodModifier modifier, Type type, ParameterDeclaration[] parameters, bool isMagic = false)
        {
            this.name = name;
            this.scope = scope;
            this.modifier = modifier;
            this.type = type;
            this.isMagic = isMagic;
            this.parameters = parameters;
        }

        #region Copy

        public void Copy(MethodDeclaration other)
        {
            name = other.name;
            scope = other.scope;
            modifier = other.modifier;
            type = other.type;
            isMagic = other.isMagic;
            hasOptionalOverride = other.hasOptionalOverride;
            isOverridden = other.isOverridden;
        }

        #endregion
    }
}