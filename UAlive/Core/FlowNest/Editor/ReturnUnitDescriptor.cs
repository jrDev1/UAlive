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
            if (Images.return_icon_32 != null) return EditorTexture.Single(Images.return_icon_32);
            return base.DefaultIcon();
        }

        protected override EditorTexture DefinedIcon()
        {
            Images.Cache();
            if (Images.return_icon_32 != null) return EditorTexture.Single(Images.return_icon_32);
            return base.DefinedIcon();
        }
    }
}