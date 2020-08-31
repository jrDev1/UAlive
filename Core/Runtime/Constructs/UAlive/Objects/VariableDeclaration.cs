using Lasm.OdinSerializer;
using Ludiq;
using System;
using UnityEditor;

namespace Lasm.UAlive
{
    [Serializable]
    public sealed class VariableDeclaration : ICopy<VariableDeclaration>
    {
        #region Ass

        [Serialize]
        private Variable _variable;
        public Variable variable => _variable = _variable ?? AssetDatabase.LoadAssetAtPath<Variable>(AssetDatabase.GUIDToAssetPath(guid));

        [Serialize]
        public string guid;
        [Serialize]
        public string classGUID;

        #endregion

        [Serialize]
        public string name = "Variable " + new object().GetHashCode().ToString();

        [Serialize]
        private Type _type = typeof(int);
        [Inspectable]
        public Type type
        {
            get => _type;
            set
            {
                var changed = _type != value;
                _type = value;
                if (changed)
                {
                    this.defaultValue = value.Default();
                    onChanged?.Invoke();
                }
            }
        }

        [Serialize]
        [Inspectable]
        public object defaultValue = 0;

        public event Action onChanged = new Action(() => { });

        public void Changed()
        {
            onChanged?.Invoke();
        }

        public void Copy(VariableDeclaration other)
        {
            name = other.name;
            type = other.type;
            guid = other.guid;
            classGUID = other.classGUID;
        }

        public static bool operator ==(VariableDeclaration original, VariableDeclaration other)
        {
            if (original.guid != other?.guid) return false;
            return true;
        }
         
        public static bool operator !=(VariableDeclaration original, VariableDeclaration other)
        {
            if (original.guid != other?.guid) return true;
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
    }
} 