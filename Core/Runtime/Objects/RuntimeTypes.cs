﻿using Lasm.OdinSerializer;
using UnityEngine;

namespace Lasm.UAlive
{
    public sealed class RuntimeTypes : SerializedMonoBehaviour
    {
        private static RuntimeTypes _instance;
        public static RuntimeTypes instance
        {
            get
            {
                if (_instance == null)
                {
                    var obj = GameObject.Find("RuntimeTypes");
                    if (obj == null || obj.GetComponent<RuntimeTypes>() == null)
                    {
                        obj = new GameObject("RuntimeTypes");
                        _instance = obj.AddComponent<RuntimeTypes>();
                    }
                    else
                    {
                        _instance = obj.GetComponent<RuntimeTypes>();
                    }
                }

                return _instance;
            }
        }

        public TypeReference references;
    }
}