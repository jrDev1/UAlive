using Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [Descriptor(typeof(SetClassVariableUnit))]
    public sealed class SetClassVariableUnitDescriptor : UnitDescriptor<SetClassVariableUnit>
    {
        public SetClassVariableUnitDescriptor(SetClassVariableUnit target) : base(target)
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

            return EditorTexture.Single(Images.class_variable_32);
        }

        protected override EditorTexture DefinedIcon()
        {
            Images.Cache();

            return EditorTexture.Single(Images.class_variable_32);
        }
    }
}
