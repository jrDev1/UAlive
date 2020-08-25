using Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [Descriptor(typeof(InvokeUnit))]
    public sealed class InvokeUnitDescriptor : UnitDescriptor<InvokeUnit>
    {
        public InvokeUnitDescriptor(InvokeUnit target) : base(target)
        {
        }

        protected override void DefinedPort(IUnitPort port, UnitPortDescription description)
        {
            base.DefinedPort(port, description);
            if (port.key == "chain" || port.key == "target") description.showLabel = false;
        }

        protected override EditorTexture DefaultIcon()
        {
            Images.Cache();

            return EditorTexture.Single(Images.invoke_32);
        }

        protected override EditorTexture DefinedIcon()
        {
            Images.Cache();

            return EditorTexture.Single(Images.invoke_32);
        }
    }
}
