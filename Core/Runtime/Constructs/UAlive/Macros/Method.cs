using UnityEngine;
using Ludiq;
using Bolt;
using System;
using System.Collections.Generic;
using Lasm.OdinSerializer;
using UnityEditor;

namespace Lasm.UAlive
{
    [Serializable]
    public class Method : Macro<FlowGraph>
#if UNITY_EDITOR
        , IUnityInitializable
#endif
    {
        [Serialize]
        public EntryUnit entry = new EntryUnit();

        public override FlowGraph DefaultGraph()
        {
            return new FlowGraph();
        }

        #region Editor

        public bool isOpen;

        #endregion


        #region Initialization
#if UNITY_EDITOR
        [SerializeField]
        private bool _isInitialized;
        public bool isInitialized { get => _isInitialized; private set => _isInitialized = value; }
         
        public void Initialize(CustomType owner, object data = null)
        {
            entry.@class = owner as CustomClass;
            this.hideFlags = HideFlags.HideInHierarchy;
            graph.units.Add(entry);
            if (data != null) entry.returnMethod = data as Action<object>;
        }

        public static Method Create(CustomType owner, Action<object> returnMethod = null)
        {
            var method = ScriptableObject.CreateInstance<Method>();
            AssetDatabase.AddObjectToAsset(method, owner);
            method.Initialize(owner, returnMethod); 
            return method;    
        }
#endif

        #endregion

        public void Invoke(IUAClass @class, Action<object> returnMethod, params object[] parameters)
        {
            entry.returnMethod = returnMethod;

            Flow flow = Flow.New(GraphReference.New(this, false));

            entry.assignedValues.Clear();

            flow.variables.Set("#secret_uaclass_instance", @class);

            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    entry.assignedValues.Add(parameters[i]);
                    flow.SetValue(entry.valueOutputs[i], parameters[i]);
                }
            }

            flow.Invoke(entry?.invoke);
        }
    }
}