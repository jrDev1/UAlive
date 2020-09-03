using Lasm.OdinSerializer;
using Ludiq;
using System;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    [Serializable]
    public sealed class VariableDeclaration : ICopy<VariableDeclaration>
    {
        #region Assets

        public string guid;
        public string classGUID;

        #endregion

        public string name = "Variable " + new object().GetHashCode().ToString();
          
        [Serialize]
        public Type _type = typeof(object); 
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
    }
} 