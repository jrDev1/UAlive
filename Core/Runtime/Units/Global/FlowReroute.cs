using Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [UnitOrder(101)]
    [RenamedFrom("Lasm.BoltExtensions.FlowReroute")]
    public sealed class FlowReroute : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput input;
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput output;

        protected override void Definition()
        {
            input = ControlInput("in", (flow) => { return output; });
            output = ControlOutput("out");
            Succession(input, output);
        }
    }
}