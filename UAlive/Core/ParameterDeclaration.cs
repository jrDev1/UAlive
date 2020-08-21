using System;

namespace Lasm.UAlive
{
    public sealed class ParameterDeclaration
    {
        public string name;
        public Type type = typeof(object);

        public ParameterDeclaration(string name, Type type)
        {
            this.name = name;
            this.type = type;
        }

        public ParameterDeclaration() { }
    }
}