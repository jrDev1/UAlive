using Ludiq;
using Lasm.OdinSerializer;
using System;
using UnityEngine;

namespace Lasm.UAlive
{
    [Serializable]
    [Inspectable]
    public sealed class RuntimeVariable : ISerializationCallbackReceiver
    {
        [SerializeField]
        public string name;
        [SerializeField]
        public string referenceGUID;
        [SerializeField]
        public Variable reference;
        [SerializeField]
        public CustomClass classReference;
        [SerializeField]
        private byte[] bytes;
        [Serialize]
        public object backingValue;
        [Inspectable]
        public object value
        {
            get
            {
                if (backingValue != null && backingValue.GetType() != reference.declaration.type) backingValue = reference.declaration.defaultValue;
                return backingValue;
            } 

            set
            { 
                backingValue = value;
            }
        }

        public void OnAfterDeserialize()
        {
            if (bytes != null) backingValue = SerializationUtility.DeserializeValue<object>(bytes, DataFormat.JSON);
        }

        public void OnBeforeSerialize()
        {
            bytes = SerializationUtility.SerializeValue<object>(backingValue, DataFormat.JSON);
        }
    }
}