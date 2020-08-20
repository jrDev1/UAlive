using UnityEditor;

namespace Lasm.UAlive
{
    public static class EnumExtensions
    {
        public static void Save(string guid, EnumMacro target, string code)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var finalPath = path.Remove(path.LastIndexOf("/") + 1, path.Length - path.LastIndexOf("/") - 1);
            code.Save().Custom(finalPath, target.title + ".cs").Text();
        }
    }
}