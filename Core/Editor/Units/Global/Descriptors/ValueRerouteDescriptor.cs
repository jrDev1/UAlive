using Bolt;
using Ludiq;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    [Descriptor(typeof(ValueReroute))]
    public sealed class ValueRerouteDescriptor : UnitDescriptor<ValueReroute>
    {
        public ValueRerouteDescriptor(ValueReroute target) : base(target)
        {
        }

        protected override void DefinedPort(IUnitPort port, UnitPortDescription description)
        {
            base.DefinedPort(port, description);
            
            description.showLabel = false;
        }

        protected override EditorTexture DefaultIcon()
        {
            Images.Cache();
            return EditorTexture.Single(Images.value_reroute_32);
        }

        protected override EditorTexture DefinedIcon()
        {
            Images.Cache();
            return EditorTexture.Single(Images.value_reroute_32);
        }
    }
} 