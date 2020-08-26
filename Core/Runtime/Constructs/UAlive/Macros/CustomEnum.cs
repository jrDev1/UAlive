using Ludiq;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    [Serializable]
    public sealed class CustomEnum : CustomType
    {
        [Inspectable]
        public List<EnumItem> items = new List<EnumItem>();

        #region Declaration

        public override string GetDefaultName()
        {
            return "New Custom Enum";
        }

        #endregion
    }
}