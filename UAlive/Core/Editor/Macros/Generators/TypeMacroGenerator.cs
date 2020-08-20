namespace Lasm.UAlive
{
    public abstract class TypeMacroGenerator<TGenerator, TMacro> : Decorator<TGenerator, GeneratorAttribute, TMacro>
        where TGenerator : TypeMacroGenerator<TGenerator, TMacro>
            where TMacro : TypeMacro
    {
        protected abstract void DefineCompiledCode();
        protected abstract void DefineLiveCode();
                
        public void GenerateLiveCode()
        {
            BeforeLiveGeneration();
            DefineLiveCode();
            AfterLiveGeneration();
        }

        public void GenerateCompiledCode()
        {
            BeforeCompiledGeneration();
            DefineCompiledCode();
            AfterCompiledGeneration();
        }


        protected abstract void AfterLiveGeneration();
        protected abstract void AfterCodeGeneration();

        protected abstract void BeforeLiveGeneration();
        protected abstract void BeforeCodeGeneration();

        public void BeforeCompiledGeneration()
        {
        }

        public void AfterCompiledGeneration()
        {
        }
    }
}