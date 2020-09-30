using System;
using UnityEditor;

namespace Lasm.UAlive
{
    public abstract class Definer<T> : Decorator<Definer<T>, DefinerAttribute, T>, IRefreshable, IDefinable
    {
        public event Action definitionChanged = () => { };
        public event Action refreshed = () => { };

        public void Changed()
        {
            definitionChanged.Invoke();
        }

        public virtual void Define()
        {
            Refresh();
        }

        public abstract void Undefine();

        protected bool _defineAdded;
        public bool defineAdded { get => _defineAdded; private set => _defineAdded = value; }

        protected bool _defineRemoved;
        public bool defineRemoved { get => _defineRemoved; private set => _defineRemoved = value; }

        public virtual bool changed => defineAdded || defineRemoved;

        private CustomType GetRootAsset(CustomType instance)
        {
            return AssetDatabase.LoadAssetAtPath<CustomType>(AssetDatabase.GUIDToAssetPath(instance.GetGUID()));
        }

        public virtual void Refresh()
        {
            defineAdded = false;
            defineRemoved = false;
            refreshed();
        }
    }
}