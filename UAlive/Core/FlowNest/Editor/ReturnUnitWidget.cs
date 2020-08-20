using Ludiq;
using Bolt;

namespace Lasm.UAlive
{
    [Widget(typeof(ReturnUnit))]
    public class ReturnUnitWidget : UnitWidget<ReturnUnit>
    {
        public ReturnUnitWidget(FlowCanvas canvas, ReturnUnit unit) : base(canvas, unit)
        {

        }

        protected override NodeColorMix color => new NodeColorMix(NodeColor.Blue) { red = 0.3f };
    }
}