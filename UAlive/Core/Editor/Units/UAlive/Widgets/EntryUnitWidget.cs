using System.Collections;
using System.Collections.Generic;
using Ludiq;
using Bolt;

namespace Lasm.UAlive
{
    [Widget(typeof(EntryUnit))]
    public class EntryUnitWidget : UnitWidget<EntryUnit>
    {
        public EntryUnitWidget(FlowCanvas canvas, EntryUnit unit) : base(canvas, unit)
        {

        }

        public override bool canCopy => false;
        public override bool canDelete => false;

        protected override NodeColorMix color => new NodeColorMix(NodeColor.Blue) { red = 0.3f };
    }
}