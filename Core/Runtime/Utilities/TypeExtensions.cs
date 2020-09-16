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
            var path = Selection.activeObject == null || Selection.assetGUIDs.Length < 2 ? HUMAssets.ProjectWindowPath() : AssetDatabase.GUIDToAssetPath(Selection.activeObject.GetGUID());
            AssetDatabase.CreateAsset(macro, path + "/" + macro.GetDefaultName() + ".asset");
            var definable = macro as IDefinable;
            if (definable != null) definable.Define();
            Selection.activeObject = macro;
            return macro; 
        }
    }
}