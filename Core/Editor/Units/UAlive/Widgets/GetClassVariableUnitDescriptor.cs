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

        protected override void DefinedPort(IUnitPort port, UnitPortDescription description)
        {
            base.DefinedPort(port, description);
            if (port.key == "target") description.showLabel = false;
        }

        protected override EditorTexture DefaultIcon()
        {
            Images.Cache();
            if (target.Class != null && target.Class.editorData.icon != null && target.Class.editorData.icon != Images.class_32)
            {
                return EditorTexture.Single(target.Class.editorData.icon);
            }
            return EditorTexture.Single(Images.class_variable_32);
        }

        protected override EditorTexture DefinedIcon()
        {
            Images.Cache();
            if (target.Class != null && target.Class.editorData.icon != null && target.Class.editorData.icon != Images.class_32)
            {
                return EditorTexture.Single(target.Class.editorData.icon);
            }
            return EditorTexture.Single(Images.class_variable_32);
        }

        protected override string DefinedTitle()
        {
            var human = LudiqCore.Configuration.humanNaming;
            if (target.variable != null) return (human ? "Get " : string.Empty) + target.Class.title + (human ? " : " : ".") + target.variable.name + (human ? string.Empty : " (get)");
            return base.DefinedTitle();
        }

        protected override string DefaultTitle()
        {
            var human = LudiqCore.Configuration.humanNaming;
            if (target.variable != null) return (human ? "Get " : string.Empty) + target.variable.name + (human ? string.Empty : " (get)");
            return base.DefaultTitle();
        }

        protected override string DefaultSurtitle()
        {
            if (target.Class != null) return target.Class.title;
            return base.DefinedSubtitle();
        }

        protected override string DefinedSurtitle()
        {
            if (target.Class != null) return target.Class.title;
            return base.DefinedSubtitle();
        }

        protected override string DefaultShortTitle()
        {
            var human = LudiqCore.Configuration.humanNaming;
            if (target.variable != null) return (human ? "Get " : string.Empty) + target.variable.name + (human ? string.Empty : " (get)");
            return base.DefaultShortTitle();
        }

        protected override string DefinedShortTitle()
        {
            var human = LudiqCore.Configuration.humanNaming;
            if (target.variable != null) return (human ? "Get " : string.Empty) + target.variable.name + (human ? string.Empty : " (get)");
            return base.DefinedShortTitle();
        }
    }
}
