#if UNITY_EDITOR
using System;
using System.IO;
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
            var isFolder = Directory.Exists(AssetDatabase.GUIDToAssetPath(Selection.activeObject?.GetGUID()) + "/");
            var _path = AssetDatabase.GUIDToAssetPath(Selection.activeObject.GetGUID());
            var path = Selection.activeObject == null ? HUMAssets.ProjectWindowPath() : (isFolder ? _path : _path.Remove(_path.LastIndexOf("/"), _path.Length - (_path.LastIndexOf("/"))));
            AssetDatabase.CreateAsset(macro, path + "/" + macro.GetDefaultName() + ".asset");
            var definable = macro as IDefinable;
            if (definable != null) definable.Define();
            Selection.activeObject = macro;
            return macro; 
        }
    }
}
#endif