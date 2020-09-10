
namespace Lasm.UAlive
{
    [Generator(typeof(CustomEnum))]
    public sealed class CustomEnumGenerator : CustomTypeGenerator<CustomEnumGenerator, CustomEnum>
    {
        private EnumGenerator @enum;
        private NamespaceGenerator @namespace;
        private string output;
        private string guid;

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
            @enum = EnumGenerator.Enum(decorated.title);
            guid = decorated.GetGUID();
        }

        private void AfterGeneration()
        {
            @namespace?.AddEnum(@enum);
            var usings = CodeBuilder.Using(new string[] { "Ludiq" });
            var output = (string.IsNullOrEmpty(decorated.@namespace) || string.IsNullOrWhiteSpace(decorated.@namespace)) ? usings + "\n\n" + @enum.Generate(0) : usings + "\n\n" + @namespace.Generate(0);
            EnumExtensions.Save(guid, decorated, output);
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
            for (int i = 0; i < decorated.items.Count; i++)
            {
                @enum.AddItem(decorated.items[i].name, decorated.items[i].index);
            }
        }

        protected override void SaveLive()
        {
            EnumExtensions.Save(guid, decorated, output);
        }

        protected override void SaveCompiled()
        {
            EnumExtensions.Save(guid, decorated, output);
        }
    }
}
