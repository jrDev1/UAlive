using System;
using UnityEditor;

namespace Lasm.UAlive
{
    public static class ClassExtensions
    {
        public static void Save(string guid, ClassMacro target, string code)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var finalPath = path.Remove(path.LastIndexOf("/") + 1, path.Length - path.LastIndexOf("/") - 1);
            code.Save().Custom(finalPath, target.title + ".cs").Text();
        }

        public static ClassMacro GetClass(ref ClassMacro macro, string GUID)
        {
            macro = macro ?? AssetDatabase.LoadAssetAtPath<ClassMacro>(AssetDatabase.GUIDToAssetPath(GUID));
            return macro;
        }

        public static void Invoke(IUAClass @class, string name, Action<object> returnMethod, params object[] parameters)
        {
            @class.Class.macro.methods.overrides[name].Invoke(@class, returnMethod, parameters);
        }
    }
}