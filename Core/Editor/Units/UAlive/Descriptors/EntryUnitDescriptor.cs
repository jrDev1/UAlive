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
            return EditorTexture.Single(Images.flow_icon_32);
        }

        protected override EditorTexture DefinedIcon()
        {
            Images.Cache();
            return EditorTexture.Single(Images.flow_icon_32);
        }
    }
}