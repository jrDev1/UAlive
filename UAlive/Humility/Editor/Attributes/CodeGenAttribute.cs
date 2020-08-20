using System;

namespace Lasm.UAlive
{
    public sealed class CodeGenAttribute : DecoratorAttribute
    {
        public CodeGenAttribute(Type type) : base(type)
        {
        }
    }
}