using Bolt;
using Ludiq;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    [Descriptor(typeof(FlowReroute))]
    public sealed class FlowRerouteDescriptor : UnitDescriptor<FlowReroute>
    {
        public FlowRerouteDescriptor(FlowReroute target) : base(target)
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
            return EditorTexture.Single(Images.flow_reroute_32);
        }

        protected override EditorTexture DefinedIcon()
        {
            Images.Cache();
            return EditorTexture.Single(Images.flow_reroute_32);
        }
    }
} 