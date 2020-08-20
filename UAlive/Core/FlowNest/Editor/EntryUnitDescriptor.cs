using Ludiq;
using Bolt;

namespace Lasm.UAlive
{
    [Descriptor(typeof(EntryUnit))]
    public sealed class EntryUnitDescriptor : UnitDescriptor<EntryUnit>
    {
        public EntryUnitDescriptor(EntryUnit target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            Images.Cache();
            if (Images.return_icon_32 != null) return EditorTexture.Single(Images.flow_icon_32);
            return base.DefaultIcon();
        }

        protected override EditorTexture DefinedIcon()
        {
            Images.Cache();
            if (Images.return_icon_32 != null) return EditorTexture.Single(Images.flow_icon_32);
            return base.DefinedIcon();
        }
    }
}