﻿using Bolt;
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

            return EditorTexture.Single(Images.class_variable_32);
        }

        protected override EditorTexture DefinedIcon()
        {
            Images.Cache();

            return EditorTexture.Single(Images.class_variable_32);
        }

        protected override string DefinedTitle()
        {
            if (target.variable != null) return "Get " + target.Class.title + "." + target.variable.name;
            return base.DefinedTitle();
        }

        protected override string DefaultTitle()
        {
            if (target.variable != null) return "Get " + target.Class.title + "." + target.variable.name;
            return base.DefaultTitle();
        }

        protected override string DefaultShortTitle()
        {
            if (target.variable != null) return "Get " + target.variable.name;
            return base.DefaultShortTitle();
        }

        protected override string DefinedShortTitle()
        {
            if (target.variable != null) return "Get " + target.variable.name;
            return base.DefinedShortTitle();
        }
    }
}
