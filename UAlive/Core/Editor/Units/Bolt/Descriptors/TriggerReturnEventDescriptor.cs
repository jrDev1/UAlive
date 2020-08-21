using UnityEngine;
using Ludiq;
using Bolt;
using UnityEditor;

namespace Lasm.UAlive
{
    /// <summary>
    /// A descriptor that assigns the TriggerReturnEvent icon.
    /// </summary>
    [Descriptor(typeof(TriggerReturnEvent))]
    public sealed class TriggerReturnEventDescriptor : UnitDescriptor<TriggerReturnEvent>
    {
        public TriggerReturnEventDescriptor(TriggerReturnEvent target) : base(target)
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