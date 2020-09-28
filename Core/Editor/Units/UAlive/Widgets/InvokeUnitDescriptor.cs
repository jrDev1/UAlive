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

        protected override string DefinedTitle()
        {
            var human = LudiqCore.Configuration.humanNaming;
            if (target.method != null) return target.Class.title + (human ? " : " : ".") + target.method.name;
            return base.DefinedTitle();
        }

        protected override string DefaultTitle()
        {
            var human = LudiqCore.Configuration.humanNaming;
            if (target.method != null) return human ? target.method.name.Prettify() : target.method.name;
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
            if (target.method != null) return target.method.name;
            return base.DefaultShortTitle();
        }

        protected override string DefinedShortTitle()
        {
            var human = LudiqCore.Configuration.humanNaming;
            if (target.method != null) return human ? target.method.name.Prettify() : target.method.name;
            return base.DefinedShortTitle();
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
