using Ludiq;
using System;
using UnityEngine;

namespace Lasm.UAlive
{
    [Serializable]
    public sealed class EditorClassData
    {
        [Serialize]
        public Texture2D icon;
        [Serialize]
        public bool customOpen;
        [Serialize]
        public bool overridesOpen;
        [Serialize]
        public bool customVariablesOpen;
        [Serialize]
        public bool customMethodsOpen;
        [Serialize]
        public bool methodOverridesOpen;
        [Serialize]
        public bool propertyOverridesOpen;
    }
}