﻿using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    public static class Styles
    {
        public static Color borderColor => EditorGUIUtility.isProSkin ? HUMColor.Grey(0.1f) : HUMColor.Grey(0.25f);
        public static Color backgroundColor => EditorGUIUtility.isProSkin ? HUMEditorColor.DefaultEditorBackground.Darken(0.1f) : HUMEditorColor.DefaultEditorBackground.Darken(0.2f);

        public static void Cache()
        {

        }
    }
}
