using Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [Descriptor(typeof(GetClassVariableUnit))]
    public sealed class GetClassVariableUnitDescriptor : UnitDescriptor<GetClassVariableUnit>
    {
        public GetClassVariableUnitDescriptor(GetClassVariableUnit target) : base(target)
        {
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
