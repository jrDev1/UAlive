﻿using System;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    public sealed class MethodGenerator : BodyGenerator
    {
        public AccessModifier scope;
        public MethodModifier modifier;
        public string name;
        public Type returnType;
        public List<ParameterGenerator> parameters = new List<ParameterGenerator>();
        public List<AttributeGenerator> attributes = new List<AttributeGenerator>();
        private string body;

        private MethodGenerator() { }

        public static MethodGenerator Method(AccessModifier scope, MethodModifier modifier, Type returnType, string name)
        {
            var method = new MethodGenerator();
            method.scope = scope;
            method.modifier = modifier;
            method.name = name;
            method.returnType = returnType;
            return method;
        }

        protected override sealed string GenerateBefore(int indent)
        {
            var attributes = string.Empty;
            foreach(AttributeGenerator attr in this.attributes)
            {
                attributes += attr.Generate(indent) + "\n";
            }
            var modSpace = modifier == MethodModifier.None ? string.Empty : " ";

            return attributes + CodeBuilder.Indent(indent) + scope.AsString().ToLower() + " " + modifier.AsString() + modSpace + returnType.As().CSharpName() + " " + name + CodeBuilder.Parameters(this.parameters);
        }

        protected override sealed string GenerateBody(int indent)
        {
            return string.IsNullOrEmpty(body) ? string.Empty : body.Contains("\n") ? body.Replace("\n", "\n" + CodeBuilder.Indent(indent)).Insert(0, CodeBuilder.Indent(indent)) : CodeBuilder.Indent(indent) + body;
        }

        protected override sealed string GenerateAfter(int indent)
        {
            return string.Empty; 
        }

        public MethodGenerator AddAttribute(AttributeGenerator generator)
        {
            attributes.Add(generator);
            return this;
        }

        public MethodGenerator Body(string body)
        {
            this.body = body;
            return this;
        }

        public MethodGenerator AddParameter(ParameterGenerator parameter)
        {
            parameters.Add(parameter);
            return this;
        }

        public List<string> Usings()
        {
            var usings = new List<string>();

            usings.Add(returnType.Namespace);

            for (int i = 0; i < attributes.Count; i++)
            {
                usings.MergeUnique(attributes[i].Usings());
            }

            return usings;
        }
    }
}