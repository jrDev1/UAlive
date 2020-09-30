using Ludiq;
using Lasm.OdinSerializer;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    [Serializable]
    [Inspectable]
    public sealed class RuntimeVariable : ISerializationCallbackReceiver
    {
        [SerializeField][HideInInspector]
        public string name;
        [SerializeField]
        [HideInInspector]
        public string referenceGUID;
        [SerializeField]
        private int _guid;
        public int guid
        {
            get
            {
                if (_guid == 0)
                {
                    _guid = referenceGUID.ToInt();
                }

                return _guid;
            }
        }
        [SerializeField]
        [HideInInspector]
        public Variable reference;
        [SerializeField]
        [HideInInspector]
        public CustomClass classReference;
        [SerializeField]
        [HideInInspector]
        private byte[] bytes;
        [SerializeField]
        [HideInInspector]
        private bool isUnityObject;
        [SerializeField]
        private UnityEngine.Object _value;
        [OdinSerialize]
        [HideInInspector]
        public object backingValue;
        [Inspectable]
        public object value
        {
            get
            {
                if (backingValue != null)
                {
                    if (reference != null && backingValue.GetType() != reference.declaration.type) backingValue = reference.declaration.defaultValue;
                    if (backingValue.GetType().Inherits<UnityEngine.Object>()) _value = backingValue as UnityEngine.Object;
                }

                return backingValue;
            }

            set
            {
                backingValue = value;
            }
        }


        public void OnAfterDeserialize()
        {
            if (isUnityObject)
            {
                value = _value;
                isUnityObject = false;
                return;
            }

            isUnityObject = false;
            if (bytes != null) value = SerializationUtility.DeserializeValue<object>(bytes, DataFormat.JSON);
        }

        public void OnBeforeSerialize()
        {
            if (value != null && value.GetType().Inherits<UnityEngine.Object>())
            {
                isUnityObject = true;
                return;
            }

            isUnityObject = false;
            bytes = SerializationUtility.SerializeValue<object>(value, DataFormat.JSON);
        }
    }
}