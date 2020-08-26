namespace Lasm.UAlive
{
    public interface ITypeDeclaration
    {
        string title { get; set; }
        string @namespace { get; set; }
        string GetDefaultName();
    }
}