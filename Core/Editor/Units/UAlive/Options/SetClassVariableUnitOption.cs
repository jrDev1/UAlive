﻿using Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [FuzzyOption(typeof(SetClassVariableUnit))]
    public sealed class SetClassVariableUnitOption : UnitOption<SetClassVariableUnit>
    {
        public SetClassVariableUnitOption() : base() { }

        public SetClassVariableUnitOption(SetClassVariableUnit unit) : base(unit)
        {
        }

        public override bool favoritable => false;

        protected override UnitCategory Category()
        {
            if (string.IsNullOrEmpty(unit?.Class?.title)) return base.Category();
            var @namespace = unit?.Class?.@namespace?.Replace(".", "/").Replace(@"\", "/");
            var cat = new UnitCategory(base.Category() + "/" + (string.IsNullOrEmpty(@namespace) ? "global" : @namespace) + "/" + unit?.Class?.title);
            return unit.variable == null ? base.Category() : cat;
        }

        protected override string Label(bool human)
        {
            return (human ? LudiqGUIUtility.DimString("Set ") : string.Empty) + unit.variable?.name + (human ? string.Empty : " (set)");
        }
    }

}