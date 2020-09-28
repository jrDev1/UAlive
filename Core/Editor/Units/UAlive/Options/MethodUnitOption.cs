using Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [FuzzyOption(typeof(MethodUnit))]
    public sealed class MethodUnitOption : UnitOption<MethodUnit>
    {
        public MethodUnitOption(MethodUnit unit) : base(unit)
        {
        }

        protected override UnitCategory Category()
        {
            if (string.IsNullOrEmpty(unit?.Class?.title)) return base.Category();
            var @namespace = unit?.Class?.@namespace?.Replace(".", "/").Replace(@"\", "/");
            var cat = new UnitCategory(base.Category() + "/" + (string.IsNullOrEmpty(@namespace) ? "global" : @namespace) + "/" + unit?.Class?.title);
            return unit.method == null || unit.method.entry == null ? base.Category() : cat;
        }

        protected override string Label(bool human)
        {
            return (human ? LudiqGUIUtility.DimString("Invoke ") : string.Empty) + unit.method?.name + (human ? string.Empty : " ()");
        }
    }

}