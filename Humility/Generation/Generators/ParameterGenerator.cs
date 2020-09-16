using System;

namespace Lasm.UAlive
{
    public sealed class ParameterGenerator : ConstructGenerator
    {
        public string name;
        private Type type;
        private string stringType;
        private ParameterModifier modifier;
        
        public override string Generate(int indent)
        {
            return type == null ? stringType + " " + name : type.As().CSharpName() + " " + name.LegalMemberName();
        }

        private ParameterGenerator()
        {

        }

        public static ParameterGenerator Parameter(string name, Type type, ParameterModifier modifier)
        {
            var parameter = new ParameterGenerator();
            parameter.name = name;
            parameter.type = type;
            parameter.modifier = modifier;
            return parameter;
        }

        public static ParameterGenerator Parameter(string name, string type, ParameterModifier modifier)
        {
            var parameter = new ParameterGenerator();
            parameter.name = name;
            parameter.stringType = type;
            parameter.modifier = modifier;
            return parameter;
        }
    }
}