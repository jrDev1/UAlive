
using UnityEngine;

namespace Lasm.UAlive
{
    [Generator(typeof(CustomEnum))]
    public sealed class CustomEnumGenerator : CustomTypeGenerator<CustomEnumGenerator, CustomEnum>
    {
        private EnumGenerator @enum;
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

        protected override void BeforeCompiledGeneration()
        {
            BeforeGeneration();
        }

        protected override void DefineCompiledCode()
        {
            DefineCode();
        }

        protected override void BeforeLiveGeneration()
        {
            BeforeGeneration();
        }

        protected override void AfterLiveGeneration()
        {
            AfterGeneration();
        }

        protected override void DefineLiveCode()
        {
            DefineCode();
        }

        private void BeforeGeneration()
        {
            if (!(string.IsNullOrEmpty(decorated.@namespace) || string.IsNullOrWhiteSpace(decorated.@namespace))) @namespace = NamespaceGenerator.Namespace(decorated.@namespace.ToString());
            @enum = EnumGenerator.Enum(decorated.title.LegalMemberName());
            guid = decorated.GetGUID();
        }

        private void AfterGeneration()
        {
            @namespace?.AddEnum(@enum);
            var usings = CodeBuilder.Using(new string[] { "Ludiq" });
            output = (string.IsNullOrEmpty(decorated.@namespace) || string.IsNullOrWhiteSpace(decorated.@namespace)) ? usings + "\n\n" + @enum.Generate(0) : usings + "\n\n" + @namespace.Generate(0);
        }

        private void DefineCode()
        {
            for (int i = 0; i < decorated.items.Count; i++)
            {
                @enum.AddItem(decorated.items[i].name, decorated.items[i].index);
            }
        }

        protected override void SaveLive()
        {
            var finalOutput = CodeBuilder.RemoveHighlights(output);
            finalOutput = CodeBuilder.RemoveMarkdown(finalOutput);
            EnumExtensions.Save(guid, decorated, finalOutput);
        }

        protected override void SaveCompiled()
        {
            var finalOutput = CodeBuilder.RemoveHighlights(output);
            finalOutput = CodeBuilder.RemoveMarkdown(finalOutput);
            EnumExtensions.Save(guid, decorated, finalOutput);
        }
    }
}
