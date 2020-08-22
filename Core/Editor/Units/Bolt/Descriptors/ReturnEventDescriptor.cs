using UnityEngine;
using Ludiq;
using Bolt;
using UnityEditor;

namespace Lasm.UAlive
{
    /// <summary>
    /// A descriptor that assigns the ReturnEvents icon.
    /// </summary>
    [Descriptor(typeof(ReturnEvent))]
    public sealed class ReturnEventDescriptor : EventUnitDescriptor<ReturnEvent>
    {
        public static Texture2D icon;

        public ReturnEventDescriptor(ReturnEvent target) : base(target)
        {

        }

        protected override EditorTexture DefaultIcon()
        {
            Images.Cache();
            return EditorTexture.Single(Images.return_event_32);
        }

        protected override EditorTexture DefinedIcon()
        {
            Images.Cache();
            return EditorTexture.Single(Images.return_event_32);
        }
    }
}