using System;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    [Generator(typeof(InterfaceMacro))]
    public sealed class InterfaceMacroGenerator : TypeMacroGenerator<InterfaceMacroGenerator, InterfaceMacro>
    {
        private InterfaceGenerator @interface;
        private NamespaceGenerator @namespace;
        private string output;
        private string guid;

        protected override void AfterCodeGeneration()
        {
            AfterGeneration();
        }

        protected override void AfterLiveGeneration()
        {
            AfterGeneration();
        }

        protected override void BeforeCodeGeneration()
        {
            BeforeGeneration();
        }

        private void BeforeGeneration()
        {
            if (!(string.IsNullOrEmpty(decorated.@namespace) || string.IsNullOrWhiteSpace(decorated.@namespace))) @namespace = NamespaceGenerator.Namespace(decorated.@namespace.ToString());
            @interface = InterfaceGenerator.Interface(decorated.title);
            guid = decorated.GetGUID();
        }

        private void AfterGeneration()
        {
            @namespace?.AddInterface(@interface);
            var usings = CodeBuilder.Using(@interface.Usings()) + "\n\n";
            var output = (string.IsNullOrEmpty(decorated.@namespace) || string.IsNullOrWhiteSpace(decorated.@namespace)) ? usings + @interface.Generate(0) : usings + @namespace.Generate(0);
            InterfaceExtensions.Save(guid, decorated, output);
        }

        protected override void BeforeLiveGeneration()
        {
            BeforeGeneration();
        }

        protected override void DefineCompiledCode()
        {
            DefineCode();
        }

        protected override void DefineLiveCode()
        {
            DefineCode();
        }

        private void DefineCode()
        {
            for (int i = 0; i < decorated.properties.Count; i++)
            {
                var prop = decorated.properties[i];
                @interface.AddProperty(InterfacePropertyGenerator.Property(prop.name, prop.type, prop.get, prop.set));
            }

            for (int i = 0; i < decorated.methods.Count; i++)
            {
                var method = decorated.methods[i];
                var methodGen = InterfaceMethodGenerator.Method(method.name, method.returnType);

                foreach (KeyValuePair<string, Type> pair in method.parameters)
                {
                    methodGen.AddParameter(ParameterGenerator.Parameter(pair.Key, pair.Value.As().CSharpName(fullName: true), ParameterModifier.None));
                }

                @interface.AddMethod(methodGen);
            }
        }
    }
}
