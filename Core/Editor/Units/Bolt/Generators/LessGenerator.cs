﻿using Bolt;

namespace Lasm.UAlive
{
    [UnitGenerator(typeof(Less))]
    public sealed class LessGenerator : UnitGenerator<Less>
    {
        public LessGenerator(Less unit) : base(unit)
        {
        }

        public override string GenerateValue(ValueInput input)
        {
            if (input == Unit.a)
            {
                if (Unit.a.hasAnyConnection)
                {
                    return (Unit.a.connection.source.unit as Unit).GenerateValue(Unit.a.connection.source);
                }
            }

            if (input == Unit.b)
            {
                if (Unit.b.hasAnyConnection)
                {
                    return (Unit.b.connection.source.unit as Unit).GenerateValue(Unit.b.connection.source);
                }
                else
                {
                    return Unit.numeric ? Unit.defaultValues["b"].As().Code(true) : base.GenerateValue(input);
                }
            }

            return base.GenerateValue(input);
        }

        public override string GenerateValue(ValueOutput output)
        {
            if (output == Unit.comparison)
            {
                return GenerateValue(Unit.a) + " < " + GenerateValue(Unit.b);
            }

            return base.GenerateValue(output);
        }
    }
}