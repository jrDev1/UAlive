using Bolt;
using System.Linq;
using UnityEngine;

namespace Lasm.UAlive
{
    [UnitGenerator(typeof(Literal))]
    public sealed class LiteralGenerator : UnitGenerator<Literal>
    {
        public LiteralGenerator(Literal unit) : base(unit)
        {
        }

        public override string GenerateValue(ValueOutput output)
        {
            return Unit.value.As().Code(true);
        }
    }
}