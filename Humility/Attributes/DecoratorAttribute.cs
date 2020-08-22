using System;

namespace Lasm.UAlive
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public abstract class DecoratorAttribute : Attribute
    {
        public Type type;

        public DecoratorAttribute(Type type)
        {
            this.type = type;
        }
    }
}
