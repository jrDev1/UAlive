using System;

namespace Lasm.UAlive
{
    public interface IDefinable
    {
        bool defineAdded { get; }
        bool defineRemoved { get; }
        bool changed { get; }
        event Action definitionChanged;
        void Define();
        void Undefine();
    }

    public interface IMethodOverridable
    {

    }

    public interface IPropertyOverridable
    {

    }
}