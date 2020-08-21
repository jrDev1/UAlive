using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Ludiq;
using Bolt;

namespace Lasm.UAlive
{
    /// <summary>
    /// The descriptor that sets the icon for Get Array Item.
    /// </summary>
    [Descriptor(typeof(GetArrayItem))]
    public class GetArrayItemDescriptor : UnitDescriptor<GetArrayItem>
    {
        public GetArrayItemDescriptor(GetArrayItem unit) : base(unit)
        {

        }

        protected override EditorTexture DefaultIcon()
        {
            Images.Cache();
            return EditorTexture.Single(Images.multi_array_32);
        }

        protected override EditorTexture DefinedIcon()
        {
            Images.Cache();
            Debug.Log(Images.multi_array_32);
            return EditorTexture.Single(Images.multi_array_32);
        }
    }
}