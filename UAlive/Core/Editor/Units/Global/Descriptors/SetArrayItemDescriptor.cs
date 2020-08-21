using UnityEngine;
using UnityEditor;
using Ludiq;
using Bolt;

namespace Lasm.UAlive
{
    /// <summary>
    /// The descriptor that sets the icon for Set Array Item.
    /// </summary>
    [Descriptor(typeof(SetArrayItem))]
    public sealed class SetArrayItemDescriptor : UnitDescriptor<SetArrayItem>
    {
        public SetArrayItemDescriptor(SetArrayItem unit) : base(unit)
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
            return EditorTexture.Single(Images.multi_array_32);
        }
    }
}