using System;

namespace Lasm.UAlive
{
    public sealed class GeneratorAttribute : DecoratorAttribute
    {
        public GeneratorAttribute(Type type) : base(type)
        {
        }
    }
}