using System;

namespace Lasm.UAlive
{
    public sealed class UnitGeneratorAttribute : DecoratorAttribute
    {
        public UnitGeneratorAttribute(Type type) : base(type)
        {
        }
    }
}