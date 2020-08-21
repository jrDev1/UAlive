using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using Bolt;
using UnityEditor;

namespace Lasm.UAlive
{
    /// <summary>
    /// A descriptor that assigns the EventReturns icon.
    /// </summary>
    [Descriptor(typeof(EventReturn))]
    public sealed class EventReturnDescriptor : UnitDescriptor<EventReturn>
    {
        public EventReturnDescriptor(EventReturn target) : base(target)
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