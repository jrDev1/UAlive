namespace Lasm.UAlive
{
    public abstract class CustomTypeGenerator<TGenerator, TMacro> : Decorator<TGenerator, GeneratorAttribute, TMacro>
        where TGenerator : CustomTypeGenerator<TGenerator, TMacro>
            where TMacro : CustomType
    {
        protected abstract void DefineCompiledCode();
        protected abstract void DefineLiveCode();
                
        public void GenerateLiveCode()
        {
            BeforeLiveGeneration();
            DefineLiveCode();
            AfterLiveGeneration();
            SaveLive();
        }

        public void GenerateCompiledCode()
        {
            BeforeCompiledGeneration();
            DefineCompiledCode();
            AfterCompiledGeneration();
            SaveCompiled();
        }


        protected abstract void AfterLiveGeneration();
        protected abstract void AfterCompiledGeneration();

        protected abstract void BeforeLiveGeneration();
        protected abstract void BeforeCompiledGeneration();

        protected abstract void SaveLive();
        protected abstract void SaveCompiled();
    }
}