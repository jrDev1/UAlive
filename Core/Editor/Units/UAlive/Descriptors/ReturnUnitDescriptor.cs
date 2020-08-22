using Ludiq;
using Bolt;

namespace Lasm.UAlive
{
    [Descriptor(typeof(ReturnUnit))]
    public class ReturnUnitDescriptor : UnitDescriptor<ReturnUnit>
    {
        public ReturnUnitDescriptor(ReturnUnit unit) : base(unit)
        {

        }

        protected override EditorTexture DefaultIcon()
        {
            Images.Cache();
            return EditorTexture.Single(Images.return_icon_32);
        }

        protected override EditorTexture DefinedIcon()
        {
            Images.Cache();
            return EditorTexture.Single(Images.return_icon_32);
        }
    }
}