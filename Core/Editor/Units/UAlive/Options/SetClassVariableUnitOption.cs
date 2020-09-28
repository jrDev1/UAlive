using Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [FuzzyOption(typeof(SetClassVariableUnit))]
    public sealed class SetClassVariableUnitOption : UnitOption<SetClassVariableUnit>
    {
        public SetClassVariableUnitOption(SetClassVariableUnit unit) : base(unit)
        {
        }

        protected override UnitCategory Category()
        {
            var @namespace = unit?.Class?.@namespace?.Replace(".", "/").Replace(@"\", "/");
            var cat = new UnitCategory(base.Category() + "/" + (string.IsNullOrEmpty(@namespace) ? "global" : @namespace));
            return unit.variable == null ? base.Category() : cat;
        }

        protected override string Label(bool human)
        {
            return LudiqGUIUtility.DimString("Set ") + unit.variable?.name;
        }
    }

}