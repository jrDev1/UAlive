using Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [Descriptor(typeof(ThisUnit))]
    public sealed class ThisUnitDescriptor : UnitDescriptor<ThisUnit>
    {
        public ThisUnitDescriptor(ThisUnit target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            Images.Cache();

            return EditorTexture.Single(Images.this_32);
        }

        protected override EditorTexture DefinedIcon()
        {
            Images.Cache();

            return EditorTexture.Single(Images.this_32);
        }
    }
}
