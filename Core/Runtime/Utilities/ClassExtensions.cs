using System;
using UnityEditor;

namespace Lasm.UAlive
{
    public static class ClassExtensions
    {
        public static void Save(string guid, CustomClass target, string code)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var finalPath = path.Remove(path.LastIndexOf("/") + 1, path.Length - path.LastIndexOf("/") - 1);
            code.Save().Custom(finalPath, target.title.Replace(" ", string.Empty) + ".cs").Text();
        }

        public static CustomClass GetClass(ref CustomClass macro, string GUID)
        {
            macro = macro ?? AssetDatabase.LoadAssetAtPath<CustomClass>(AssetDatabase.GUIDToAssetPath(GUID));
            return macro;
        }

        public static void Invoke(IUAClass @class, string name, Action<object> returnMethod, bool isOverride = false, params object[] parameters)
        {
            if (isOverride)
            {
                @class.Class.macro.methods.overrides.current[name].Invoke(@class, returnMethod, parameters);
                return;
            }

            for (int i = 0; i < @class.Class.macro.methods.custom.Count; i++)
            {
                var instance = @class.Class.macro.methods.custom[i];
                if (instance.name == name)
                {
                    instance.Invoke(@class, returnMethod, parameters);
                    return;
                }
            }
            
        }
    }
}