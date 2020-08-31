namespace Lasm.UAlive
{
    public interface IUnityInitializable
    {
        bool isInitialized { get; }
        void Initialize(CustomType owner, object data = null);
    }
} 