using Ludiq;
using System;
using System.Linq;
using UnityEditor;

namespace Lasm.UAlive
{
    [Serializable]
    [IncludeInSettings(false)]
    public abstract class CustomType : LudiqScriptableObject, ITypeDeclaration
    {
        #region Declaration

        [Serialize]
        private string _title;
        public virtual string title { get => _title; set => _title = value; }

        [Serialize]
        private string _namespace;
        public virtual string @namespace { get => _namespace; set => _namespace = value; }

        public abstract string GetDefaultName();

        #endregion

#if UNITY_EDITOR
        private void OnDisable()
        {
            DeserializationRoutine.Disable();
        }
#endif
    }
}