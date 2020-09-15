using Ludiq;
using System;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    [Generator(typeof(CustomInterface))]
    public sealed class CustomInterfaceGenerator : CustomTypeGenerator<CustomInterfaceGenerator, CustomInterface>
    {
        private InterfaceGenerator @interface;
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

        protected override void AfterCompiledGeneration()
        {
            AfterGeneration();
        }

        protected override void AfterLiveGeneration()
        {
            AfterGeneration();
        }

        protected override void BeforeCompiledGeneration()
        {
            BeforeGeneration();
        }

        private void BeforeGeneration()
        {
            if (!(string.IsNullOrEmpty(decorated.@namespace) || string.IsNullOrWhiteSpace(decorated.@namespace))) @namespace = NamespaceGenerator.Namespace(decorated.@namespace.ToString());
            @interface = InterfaceGenerator.Interface(decorated.title);
            @interface.AddAttribute(AttributeGenerator.Attribute<IncludeInSettingsAttribute>().AddParameter(true));
            guid = decorated.GetGUID();
        }

        private void AfterGeneration()
        {
            @namespace?.AddInterface(@interface);
            var usings = CodeBuilder.Using(@interface.Usings()) + "\n\n";
            output = (string.IsNullOrEmpty(decorated.@namespace) || string.IsNullOrWhiteSpace(decorated.@namespace)) ? usings + @interface.Generate(0) : usings + @namespace.Generate(0);
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

        protected override void SaveLive()
        {
            InterfaceExtensions.Save(guid, decorated, output);
        }

        protected override void SaveCompiled()
        {
            InterfaceExtensions.Save(guid, decorated, output);
        }
    }
}
