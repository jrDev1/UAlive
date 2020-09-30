using Bolt;
using Ludiq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    [Serializable]
    public sealed class CustomClass : CustomType
    {
        #region Declaration

        public override string GetDefaultName()
        {
            return "Custom Class " + this.GetInstanceID().ToString().Replace("-", string.Empty);
        }

        [Serialize]
        public Inheritance inheritance { get; set; } = new Inheritance();

        #endregion

        #region Members

        [Serialize]
        public Methods methods = new Methods();

        [Serialize]
        public Variables variables = new Variables();

        #endregion

        #region EDITOR ONLY
#if UNITY_EDITOR
        public EditorClassData editorData = new EditorClassData();
#endif
        #endregion
    }
}
