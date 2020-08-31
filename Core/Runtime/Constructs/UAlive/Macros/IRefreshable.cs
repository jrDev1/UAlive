using System;

namespace Lasm.UAlive
{
    public interface IRefreshable
    {
        void Refresh();
        event Action refreshed;
    }
}