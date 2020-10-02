using Bolt;
using Ludiq;
using System.Linq;

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
            if (graph != null)
            {
                var entry = graph.units.OfType<EntryUnit>().ToList()[0];

                if (entry != null && entry.@class.inheritance.compiledType != null)
                {
                    @this = ValueOutput(entry.@class.inheritance.compiledType, "this", (flow) =>
                    {
                        return flow.variables.Get("#secret_uaclass_instance");
                    });

                    return;
                }
            }

            @this = ValueOutput<IUAClass>("this", (flow) =>
            {
                return (IUAClass)flow.variables.Get("#secret_uaclass_instance");
            });
        }
    }
}