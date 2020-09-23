using Bolt;

namespace Lasm.UAlive
{
    [UnitGenerator(typeof(SelectUnit))]
    public sealed class SelectGenerator : UnitGenerator<SelectUnit>
    {
        public SelectGenerator(Unit unit) : base(unit)
        {
        }

        public override string GenerateValue(ValueOutput output)
        {
            if (output == Unit.selection)
            {
                var str = string.Empty;
                var @true = base.GenerateValue(Unit.ifTrue);
                var @false = base.GenerateValue(Unit.ifFalse);
                var condition = base.GenerateValue(Unit.condition);

                if (Unit.condition.hasValidConnection)
                {
                    condition = ((Unit)Unit.condition.connection.source.unit).GenerateValue(Unit.condition.connection.source);
                }

                if (Unit.ifTrue.hasValidConnection)
                {
                    @true = ((Unit)Unit.ifTrue.connection.source.unit).GenerateValue(Unit.ifTrue.connection.source);
                }
                else
                {

                }

                if (Unit.ifFalse.hasValidConnection)
                {
                    @false = ((Unit)Unit.ifFalse.connection.source.unit).GenerateValue(Unit.ifFalse.connection.source);
                }

                str = "(" + condition + " ? " + @true + " : " + @false + ")";
                return str;
            }

            return base.GenerateValue(output);
        }

        public override string GenerateValue(ValueInput input)
        {
            if (input == Unit.ifTrue)
            {

            }

            if (input == Unit.ifFalse)
            {

            }

            return base.GenerateValue(input);
        }
    }
}