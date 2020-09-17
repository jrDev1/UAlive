using Bolt;

namespace Lasm.UAlive
{
    [UnitGenerator(typeof(Branch))]
    public sealed class BranchGenerator : UnitGenerator<Branch>
    {
        public BranchGenerator(Branch unit) : base(unit)
        {
        }

        public override string GenerateControl(ControlInput input, int indent)
        {
            var output = string.Empty;

            if (input == Unit.enter)
            {
                output += CodeBuilder.Indent(indent) + "if (" + GenerateValue(Unit.condition) + ")";
                output += "\n";
                output += CodeBuilder.OpenBody(indent);
                output += "\n";
                output += (Unit.ifTrue.hasAnyConnection ? (Unit.ifTrue.connection.destination.unit as Unit).GenerateControl(Unit.ifTrue.connection.destination, indent + 1) : string.Empty);
                output += "\n";
                output += CodeBuilder.CloseBody(indent);

                if (Unit.ifFalse.hasAnyConnection)
                {
                    output += "\n";
                    output += CodeBuilder.Indent(indent) + "else";
                    output += "\n";
                    output += CodeBuilder.OpenBody(indent);
                    output += "\n";
                    output += (Unit.ifFalse.hasAnyConnection ? (Unit.ifFalse.connection.destination.unit as Unit).GenerateControl(Unit.ifFalse.connection.destination, indent + 1) : string.Empty);
                    output += "\n";
                    output += CodeBuilder.CloseBody(indent);
                }
            }

            return output;
        }

        public override string GenerateValue(ValueInput input)
        {
            if (input == Unit.condition)
            {
                if (Unit.condition.hasAnyConnection)
                {
                    return (Unit.condition.connection.source.unit as Unit).GenerateValue(Unit.condition.connection.source);
                }
            }

            return base.GenerateValue(input);
        }
    }
}