﻿using System.Collections.Generic;

namespace Lasm.UAlive
{
    public sealed class ConstructorGenerator : BodyGenerator
    {
        private AccessModifier scope;
        private ConstructorModifier modifier;
        private string name;
        private List<(bool hasBase, ParameterGenerator generator)> parameters = new List<(bool hasBase, ParameterGenerator generator)>();
        private List<AttributeGenerator> attributes = new List<AttributeGenerator>();
        private string body;

        private ConstructorGenerator() { }

        public static ConstructorGenerator Constructor(AccessModifier scope, ConstructorModifier modifier, string name)
        {
            var constructor = new ConstructorGenerator();
            constructor.scope = scope;
            constructor.modifier = modifier;
            constructor.name = name;
            return constructor;
        }

        protected override sealed string GenerateBefore(int indent)
        {
            var attributes = string.Empty;
            foreach (AttributeGenerator attr in this.attributes)
            {
                attributes += attr.Generate(indent) + "\n";
            }
            var modSpace = modifier == ConstructorModifier.None ? string.Empty : " ";
            var parameters = string.Empty;
            var baseParameters = string.Empty;

            for (int i = 0; i < this.parameters.Count; i++)
            {
                parameters += this.parameters[i].generator.Generate(0);
                if (i < this.parameters.Count - 1) parameters += ", ";
                if (this.parameters[i].hasBase)
                {
                    baseParameters += this.parameters[i].generator.name;
                    if (i < this.parameters.Count - 1) baseParameters += ", ";
                }
            }

            return attributes + CodeBuilder.Indent(indent) + scope.AsString().ToLower() + " " + modifier.AsString() + modSpace + name.LegalMemberName() + "(" + parameters + ")" + (string.IsNullOrEmpty(baseParameters) ? string.Empty : " : base(" + baseParameters + ")");
        }

        protected override sealed string GenerateBody(int indent)
        {
            return string.IsNullOrEmpty(body) ? string.Empty : body.Contains("\n") ? body.Replace("\n", "\n" + CodeBuilder.Indent(indent)).Insert(0, CodeBuilder.Indent(indent)) : CodeBuilder.Indent(indent) + body;
        }

        protected override sealed string GenerateAfter(int indent)
        {
            return string.Empty;
        }

        public ConstructorGenerator Body(string body)
        {
            this.body = body;
            return this;
        }

        public ConstructorGenerator AddParameter(bool hasBase, ParameterGenerator parameter)
        {
            parameters.Add((hasBase, parameter));
            return this;
        }
    }
}