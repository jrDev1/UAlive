using System;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace Lasm.UAlive
{
    public static class TypeExtensions
    {
        public static T CreateAsset<T>() where T : ScriptableObject, ITypeDeclaration
        {
            var macro = ScriptableObject.CreateInstance<T>();
            macro.title = macro.GetDefaultName();
            AssetDatabase.CreateAsset(macro, HUMAssets.ProjectWindowPath() + "/" + macro.GetDefaultName() + ".asset");
            var definable = macro as IDefinable;
            if (definable != null) definable.Define();
            return macro;
        }
    }
}