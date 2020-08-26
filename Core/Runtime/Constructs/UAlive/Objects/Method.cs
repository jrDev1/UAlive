using System;
using Ludiq;
using Bolt;
using UnityEngine;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    /// <summary>
    /// A nested graph that is invokable without a GameObject. Can be used in custom scripts, has a Property Drawer, and Bolt Inspector.
    /// </summary>
    [Serializable]
    public sealed class Method 
    {
        #region Variables
        [Serialize]
        public int id = new object().GetHashCode();
        [Serialize]
        public string parentGUID;
        [Serialize]
        public MethodMacro macro;
        [Serialize]
        public bool isInitialized;
        [Serialize]
        public bool hasOptionalOverride
        {
            get
            {
                if (macro != null) return macro.hasOverride;
                return false;
            }

            set
            {
                if (macro != null)
                {
                    macro.hasOverride = value;
                }
            }
        }

        [Serialize]
        public AccessModifier scope;
        [Serialize]
        public MethodModifier modifier;
        [Serialize]
        private string _name;
        public string name { get => _name; set { _name = value; macro.name = value; } }

        public IGraphNest nest => throw new NotImplementedException();

        private CustomClass @class;

        [Serialize]
        public bool showLabel = true;
        #endregion

#if UNITY_EDITOR         
        public Texture2D icon;
#endif 

        public void Initialize() { New(this); isInitialized = true; }

        /// <summary>
        /// Initialized a new Flow Nest with a Flow Graph and an Entry Unit, can be a Macro or an Embed as the source.
        /// </summary>
        /// <param name="source"></param> 
        public static void New(Method nest)
        {
            if (nest.macro == null) nest.macro = MethodMacro.Create();
        }

        public static void SetReturnMethod(Method nest, Action<object> returnMethod)
        {
            if (nest.macro != null)
            {
                nest.macro.entry.returnMethod = returnMethod;
            }
        }

        public void Invoke(IUAClass @class, Action<object> returnMethod, params object[] parameters)
        {
            SetReturnMethod(this, returnMethod);

            Flow flow = Flow.New(GraphReference.New(macro, false));

            macro.entry.assignedValues.Clear();

            flow.variables.Set("#secret_uaclass_instance", @class);

            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    macro.entry.assignedValues.Add(parameters[i]);
                    flow.SetValue(macro.entry.valueOutputs[i], parameters[i]);
                }
            }

            flow.Invoke(macro?.entry?.invoke);
        }
    }
}