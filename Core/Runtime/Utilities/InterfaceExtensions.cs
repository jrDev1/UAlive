using UnityEditor;

namespace Lasm.UAlive
{
    public static class InterfaceExtensions
    {
        public static void Save(string guid, InterfaceMacro target, string code)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var finalPath = path.Remove(path.LastIndexOf("/") + 1, path.Length - path.LastIndexOf("/") - 1);
            code.Save().Custom(finalPath, target.title + ".cs").Text();
        }
    }
}