﻿using Bolt;
using Ludiq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lasm.UAlive
{
    [Generator(typeof(CustomClass))]
    public class CustomClassGenerator : CustomTypeGenerator<CustomClassGenerator, CustomClass>
    {
        private ClassGenerator @class;
        private NamespaceGenerator @namespace;
        private string output;
        private string guid;

        public string GetLiveOutput()
        {
            BeforeLiveGeneration();
            DefineLiveCode();
            AfterLiveGeneration();
            return output;
        }

        public string GetCompiledOutput()
        {
            BeforeCompiledGeneration();
            DefineCompiledCode();
            AfterCompiledGeneration();
            return output;
        }

        protected override void SaveCompiled()
        {
            var finalOutput = CodeBuilder.RemoveHighlights(output);
            finalOutput = CodeBuilder.RemoveMarkdown(finalOutput);
            ClassExtensions.Save(guid, decorated, finalOutput);
        }

        protected override void SaveLive()
        {
            var finalOutput = CodeBuilder.RemoveHighlights(output);
            finalOutput = CodeBuilder.RemoveMarkdown(finalOutput);
            ClassExtensions.Save(guid, decorated, finalOutput, (type)=> { decorated.inheritance.compiledName = type; });
        }

        protected override void BeforeLiveGeneration()
        {
            if (!(string.IsNullOrEmpty(decorated.@namespace) || string.IsNullOrWhiteSpace(decorated.@namespace))) @namespace = NamespaceGenerator.Namespace(decorated.@namespace.ToString().LegalMemberName());
            @class = ClassGenerator.Class(
                RootAccessModifier.Public,
                ClassModifier.None,
                decorated.title.LegalMemberName(),
                decorated.inheritance.type);
            @class.AddAttribute(AttributeGenerator.Attribute<IncludeInSettingsAttribute>().AddParameter(true));
            @class.AddAttribute(AttributeGenerator.Attribute<InspectableAttribute>());
            @class.AddInterface(typeof(IUAClass));
            guid = decorated.GetGUID();
        }

        protected override void AfterLiveGeneration()
        {
            var uaClass = FieldGenerator.Field(AccessModifier.Private, FieldModifier.None, typeof(UAClass), "data").CustomDefault("new UAClass(" + guid.As().Code(false) + ");");
            uaClass.AddAttribute(AttributeGenerator.Attribute<SerializeField>());
            uaClass.AddAttribute(AttributeGenerator.Attribute<InspectableAttribute>());

            var interfaceUAClass = PropertyGenerator.Property(AccessModifier.Public, PropertyModifier.None, typeof(UAClass), "Class", false).SingleStatementGetter(AccessModifier.Public, "data");
            @class.AddField(uaClass);
            @class.AddProperty(interfaceUAClass);

            @namespace?.AddClass(@class);
            var usings = CodeBuilder.Using(@class.Usings()) + "\n\n";
            output = (string.IsNullOrEmpty(decorated.@namespace) || string.IsNullOrWhiteSpace(decorated.@namespace)) ? usings + @class.Generate(0) : usings + @namespace.Generate(0);
        }

        protected override void DefineCompiledCode()
        {
            var methods = decorated.methods.custom;
            var fields = decorated.variables.variables;

            for (int i = 0; i < fields.Count; i++)
            {
                var variable = decorated.variables.variables[i];
                var field = FieldGenerator.Field(AccessModifier.Public, FieldModifier.None, variable.declaration.type, variable.name);
                field.Default(variable.declaration.defaultValue);
                @class.AddField(field);
            }

            for (int i = 0; i < methods.Count; i++)
            {
                var nest = decorated.methods.custom[i];
                if (CanAddMethod(nest))
                {
                    var controlData = new ControlGenerationData();
                    controlData.returns = nest.entry.declaration.type;
                    var body = nest.entry.invoke.hasAnyConnection ? (nest.entry.invoke.connection.destination?.unit as Unit).GenerateControl(nest.entry.invoke.connection.destination, controlData, 0) : string.Empty;
                    var method = Method(nest.name, nest.entry.declaration.scope, nest.entry.declaration.modifier, nest.entry.declaration.type, body: body);
                    AddParameters(method, nest);
                    @class.AddMethod(method);
                }
            }

            var keys = decorated.methods.overrides.Keys().ToArray();

            for (int i = 0; i < keys.Length; i++)
            {
                var nest = decorated.methods.overrides.current[keys[i]];
                if (CanAddMethod(nest))
                {
                    var controlData = new ControlGenerationData();
                    controlData.returns = nest.entry.declaration.type;
                    var body = nest.entry.invoke.hasAnyConnection? (nest.entry.invoke.connection.destination?.unit as Unit).GenerateControl(nest.entry.invoke.connection.destination, controlData, 0) : string.Empty;

                    var method = nest.entry.declaration.type.Is().Void() ?
                        Method(
                            nest.name.Replace(" ", string.Empty),
                            nest.entry.declaration.scope,
                            MustOverride(nest) ? MethodModifier.Override : nest.entry.declaration.modifier,
                            nest.entry.declaration.type,
                            body: body
                            )
                        : Method(
                            nest.name.Replace(" ", string.Empty),
                            nest.entry.declaration.scope,
                            MustOverride(nest) ? MethodModifier.Override : nest.entry.declaration.modifier,
                            nest.entry.declaration.type,
                            body: body
                            );
                    AddParameters(method, nest);
                    @class.AddMethod(method);
                }
            };
        }

        protected override void DefineLiveCode()
        {
            var keys = decorated.methods.overrides.Keys().ToArray();

            for (int i = 0; i < keys.Length; i++)
            { 
                var nest = decorated.methods.overrides.current[keys[i]]; 
                if (CanAddMethod(nest))
                {
                    var method = nest.entry.declaration.type.Is().Void() ? 
                        Method(
                            nest.name.Replace(" ", string.Empty),
                            nest.entry.declaration.scope,
                            MustOverride(nest) ? MethodModifier.Override : nest.entry.declaration.modifier, 
                            nest.entry.declaration.type, 
                            true
                            )
                        : Method(
                            nest.name.Replace(" ", string.Empty),
                            nest.entry.declaration.scope,
                            MustOverride(nest) ? MethodModifier.Override : nest.entry.declaration.modifier, 
                            nest.entry.declaration.type,
                            true
                            );
                    AddParameters(method, nest);
                    @class.AddMethod(method);
                }
            };
        }

        private bool MustOverride(Method method)
        {
            return method.entry.declaration.modifier == MethodModifier.Virtual && method.entry.declaration.isOverridden || method.entry.declaration.modifier == MethodModifier.Abstract;
        }

        protected MethodGenerator Method(string key, AccessModifier scope, MethodModifier modifier, Type returnType, bool isOverride, string parameters = null)
        {
            var method = MethodGenerator.Method(scope, modifier, returnType, key.Replace(" ", string.Empty));
            var line1 = CodeBuilder.InitializeVariable("val", returnType);
            var line2 = CodeExtensions.Invoke(key, returnType, isOverride, parameters);
            var line3 = CodeBuilder.NullVoidOrNot(returnType, string.Empty, "\n" + CodeBuilder.Return("val"));
            method.Body(line1 + line2 + line3);
            return method;
        }

        protected MethodGenerator Method(string key, AccessModifier scope, MethodModifier modifier, Type returnType, string parameters = null, string body = null)
        {
            var method = MethodGenerator.Method(scope, modifier, returnType, key.Replace(" ", string.Empty));
            if (!string.IsNullOrEmpty(body)) method.Body(body);
            return method;
        }

        private MethodGenerator Method(string key, AccessModifier scope, MethodModifier modifier, bool isOverride, Method nest)
        {
            var method = MethodGenerator.Method(scope, modifier, typeof(void), key.Replace(" ", string.Empty));
            method.Body(CodeExtensions.Invoke(key, typeof(void), isOverride, GetParameters(method, nest)));
            return method;
        }

        protected override void AfterCompiledGeneration()
        {
            @namespace?.AddClass(@class);
            var usings = CodeBuilder.Using(@class.Usings()) + "\n\n";
            output = (string.IsNullOrEmpty(decorated.@namespace) || string.IsNullOrWhiteSpace(decorated.@namespace)) ? usings + @class.Generate(0) : usings + @namespace.Generate(0);
        }

        protected override void BeforeCompiledGeneration()
        {
            if (!(string.IsNullOrEmpty(decorated.@namespace) || string.IsNullOrWhiteSpace(decorated.@namespace))) @namespace = NamespaceGenerator.Namespace(decorated.@namespace.ToString());
            @class = ClassGenerator.Class(
                RootAccessModifier.Public,
                ClassModifier.None,
                NoSpace(decorated.title),
                decorated.inheritance.type);
            guid = decorated.GetGUID();
        }

        private bool CanAddMethod(Method method)
        {
            return method.entry.declaration.isOverridden && method.entry.declaration.hasOptionalOverride || !method.entry.declaration.hasOptionalOverride;
        }

        private void AddParameters(MethodGenerator generator, Method method)
        {
            if (method.entry.declaration.parameters != null)
            {
                foreach (ParameterDeclaration declaration in method.entry.declaration.parameters)
                {
                    generator.AddParameter(ParameterGenerator.Parameter(declaration.name, declaration.type, ParameterModifier.None));
                }
            }
        }

        private string GetParameters(MethodGenerator generator, Method method)
        {
            var parameters = new List<ParameterGenerator>();

            foreach (ParameterDeclaration declaration in method.entry.declaration.parameters)
            {
                parameters.Add(ParameterGenerator.Parameter(declaration.name, declaration.type, ParameterModifier.None));
            }

            return parameters.Count == 0 ? string.Empty : parameters.Parameters();
        }

        private string NoSpace(string str)
        {
            return str.Replace(" ", string.Empty);
        }
    }
}
