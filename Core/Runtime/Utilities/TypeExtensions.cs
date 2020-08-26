using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    public static class TypeExtensions
    {
        public static T CreateAsset<T>() where T : ScriptableObject, ITypeDeclaration
        {
            var macro = ScriptableObject.CreateInstance<T>();
            macro.title = macro.GetDefaultName();
            ProjectWindowUtil.CreateAsset(macro, HUMAssets.ProjectWindowPath() + "/" + macro.title.Nice() + ".asset");
            return macro;
        }
    }
}