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

        public bool isOverridden;
        public bool isSpecial;

        [Serialize]
        public AccessModifier scope;
        [Serialize]
        public MethodModifier modifier;
        [Serialize]
        private string _name;
        public string name { get => _name; set { _name = value; macro.name = value; } }
        [Serialize]
        public bool showLabel = true;
        [Serialize]
        private Type _type = typeof(Lasm.UAlive.Void);
        [Inspectable]
        public Type returnType
        {
            get => _type;
            set
            {
                if (macro?.entry != null)
                {
                    macro.entry.returnType = value;
                    macro.entry.Changed();
                }
                _type = value;
            }
        } 

        [Serialize]
        public Action<object> returnMethod;
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
            if (nest.macro == null) nest.macro = ScriptableObject.CreateInstance<MethodMacro>();
            nest.macro.returnMethod = nest.returnMethod;
        }
        
        public static void SetReturnMethod(Method nest)
        {
            if (nest.macro != null)
            {
                nest.macro.returnMethod = nest.returnMethod;
                nest.macro.entry.returnMethod = nest.returnMethod;
            }
        }

        public void Invoke(IUAClass @class, Action<object> returnMethod, params object[] parameters)
        { 
            SetReturnMethod(this);

            macro.entry.returnMethod = returnMethod;
            macro.returnMethod = returnMethod;
             
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

            flow.Invoke(macro.entry.invoke);
        }
    }
}