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
        private Method _method;
        public Method method => _method = _method ?? AssetDatabase.LoadAssetAtPath<Method>(AssetDatabase.GUIDToAssetPath(guid));

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

        public static bool operator ==(MethodDeclaration original, MethodDeclaration other)
        {
            if (original.name != other.name) return false;
            if (original.type != other.type) return false;
            if (original.isMagic != other.isMagic) return false;
            if (original.modifier != other.modifier) return false;
            if (original.scope != other.scope) return false;
            if (original.parameters.Length != other.parameters.Length) return false;
            for (int i = 0; i < original.parameters.Length; i++)
            {
                if (original.parameters[i] != other.parameters[i]) return false;
            }
            return true;
        }

        public static bool operator !=(MethodDeclaration original, MethodDeclaration other)
        {
            if (original.name != other.name) return true;
            if (original.type != other.type) return true;
            if (original.isMagic != other.isMagic) return true;
            if (original.modifier != other.modifier) return true;
            if (original.scope != other.scope) return true;
            if (original.parameters.Length != other.parameters.Length) return true;
            for (int i = 0; i < original.parameters.Length; i++)
            {
                if (original.parameters[i] != other.parameters[i]) return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        #endregion
    }
}