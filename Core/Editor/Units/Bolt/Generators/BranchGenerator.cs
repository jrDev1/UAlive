using Bolt;

namespace Lasm.UAlive
{
    public sealed class BranchGenerator : UnitGenerator<Branch>
    {
        public BranchGenerator(Branch unit) : base(unit)
        {
        }

        public override string GenerateControl(ControlInput input, int indent)
        {
            return string.Empty;
        }

        public override string GenerateValue(ValueInput input)
        {

            return string.Empty;
        }

        public override string GenerateValue(ValueOutput output)
        {
            return string.Empty;
        }
    }
}