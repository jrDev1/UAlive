using System;

namespace Lasm.UAlive
{
    public sealed class DefinerAttribute : DecoratorAttribute
    {
        public DefinerAttribute(Type type) : base(type)
        {
        }
    }
}