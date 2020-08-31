using Ludiq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lasm.UAlive
{
    [Generator(typeof(CustomClass))]
    public class CustomClassGenerator : CustomTypeGenerator<CustomClassGenerator, CustomClass>
    {
        private ClassGenerator @class;
        private NamespaceGenerator @namespace;
        private string output;
        private string guid;

        protected override void BeforeLiveGeneration()
        {
            if (!(string.IsNullOrEmpty(decorated.@namespace) || string.IsNullOrWhiteSpace(decorated.@namespace))) @namespace = NamespaceGenerator.Namespace(decorated.@namespace.ToString());
            @class = ClassGenerator.Class(
                RootAccessModifier.Public,
                ClassModifier.None,
                NoSpace(decorated.title),
                decorated.inheritance.type);
            var att = AttributeGenerator.Attribute<IncludeInSettingsAttribute>().AddParameter(true);
            @class.AddAttribute(att);
            @class.AddInterface(typeof(IUAClass));
            guid = decorated.GetGUID();
        }

        protected override void AfterLiveGeneration()
        {
            var uaClass = FieldGenerator.Field(AccessModifier.Private, FieldModifier.None, typeof(UAClass), "data").CustomDefault("new UAClass(" + guid.As().Code(false) + ");");
            var interfaceUAClass = PropertyGenerator.Property(AccessModifier.Public, PropertyModifier.None, typeof(UAClass), "Class", false).SingleStatementGetter(AccessModifier.Public, "data");
            @class.AddField(uaClass);
            @class.AddProperty(interfaceUAClass);

            @namespace?.AddClass(@class);
            var usings = CodeBuilder.Using(@class.Usings()) + "\n\n";
            var output = (string.IsNullOrEmpty(decorated.@namespace) || string.IsNullOrWhiteSpace(decorated.@namespace)) ? usings + @class.Generate(0) : usings + @namespace.Generate(0);
            ClassExtensions.Save(guid, decorated, output);
        }

        protected override void DefineCompiledCode()
        {

        }

        protected override void DefineLiveCode()
        {
            var keys = decorated.methods.overrides.Keys().ToArray();

            // FORCE COMPILE SHELL
            //HUMQuery.For(keys, (list, index) => 
            //{
            //    var nest = decorated.overrideMethods[keys[index]];
            //    if (CanAddMethod(nest))
            //    {
            //        var method = Method(nest.name, nest.scope, nest.modifier, nest.returnType);
            //        AddParameters(method, nest);
            //        @class.AddMethod(method);
            //    }
            //});

            for (int i = 0; i < keys.Length; i++)
            { 
                var nest = decorated.methods.overrides[keys[i]]; 
                if (CanAddMethod(nest))
                {
                    var method = nest.entry.declaration.type.Is().Void() ? 
                        Method(
                            nest.name,
                            nest.entry.declaration.scope, 
                            nest.entry.declaration.modifier, 
                            nest.entry.declaration.type, 
                            true
                            )
                        : Method(
                            nest.name, nest.entry.declaration.scope, 
                            nest.entry.declaration.modifier, 
                            nest.entry.declaration.type,
                            true
                            );
                    AddParameters(method, nest);
                    @class.AddMethod(method);
                }
            };
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

        private MethodGenerator Method(string key, AccessModifier scope, MethodModifier modifier, bool isOverride, Method nest)
        {
            var method = MethodGenerator.Method(scope, modifier, typeof(Lasm.UAlive.Void), key.Replace(" ", string.Empty));
            method.Body(CodeExtensions.Invoke(key, typeof(Lasm.UAlive.Void), isOverride, GetParameters(method, nest)));
            return method;
        }

        protected override void AfterCodeGeneration()
        {
            
        }

        protected override void BeforeCodeGeneration()
        {
            
        }

        private bool CanAddMethod(Method method)
        {
            return method.entry.declaration.isOverridden && method.entry.declaration.hasOptionalOverride || !method.entry.declaration.hasOptionalOverride;
        }

        private void AddParameters(MethodGenerator generator, Method method)
        {
            foreach (ParameterDeclaration declaration in method.entry.declaration.parameters)
            {
                generator.AddParameter(ParameterGenerator.Parameter(declaration.name, declaration.type, ParameterModifier.None));
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
