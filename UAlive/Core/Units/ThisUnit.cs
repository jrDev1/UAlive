using Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [UnitTitle("This")]
    [TypeIcon(typeof(Self))]
    public sealed class ThisUnit : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput @this;

        protected override void Definition()
        {
            @this = ValueOutput<IUAClass>("this", (flow) =>
            {
                return (IUAClass)flow.variables.Get("#secret_uaclass_instance");
            });
        }
    }
}