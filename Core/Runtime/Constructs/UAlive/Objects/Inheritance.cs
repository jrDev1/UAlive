using Ludiq;
using System;
using UnityEngine;

namespace Lasm.UAlive
{
    [Serializable][Inspectable]
    public sealed class Inheritance
    {
        [Serialize]
        private Type _type = typeof(MonoBehaviour);
        [Inspectable]
        [Ludiq.TypeFilter(Structs = false, Sealed = false, OpenConstructedGeneric = false, NonPublic = false, Obsolete = false, Enums = false, Classes = true, Abstract = true, Static = false, Interfaces = false)]
        public Type type
        {
            get => _type;
            set
            {
                _type = value;
                OnTypeChanged(value);
                OnChanged();
            }
        }

        private Type _compiledType;
        public Type compiledType
        {
            get
            {
                if (_compiledType == null && !string.IsNullOrEmpty(compiledName))
                {
                    _compiledType = Type.GetType(compiledName);
                }

                return _compiledType;
            }
        }

        public string compiledName;

        public event Action<Type> OnTypeChanged = (type) => { };
        public event Action OnChanged = () => { };

        public bool Inherits<T>()
        {
            return type.Inherits<T>();
        }

        public bool Inherits(Type type)
        {
            return type.Inherits(type);
        }
    }
}