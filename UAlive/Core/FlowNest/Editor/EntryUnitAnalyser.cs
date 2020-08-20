using Ludiq;
using Bolt;
using System.Collections.Generic;
using System.Linq;

namespace Lasm.UAlive
{
    [Analyser(typeof(EntryUnit))]
    public sealed class EntryUnitAnalyser : UnitAnalyser<EntryUnit>
    {
        public EntryUnitAnalyser(GraphReference reference, EntryUnit target) : base(reference, target)
        {
        }

        protected override IEnumerable<Warning> Warnings()
        {
            var warnings = base.Warnings().ToList();

            if (!target.invoke.hasAnyConnection)
            {
                warnings.Add(new Warning(WarningLevel.Caution, "Unit has no connections. Function may only return default or null value."));
            }
            

            return warnings;
        }
    }
}