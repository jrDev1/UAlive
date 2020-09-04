using UnityEngine;
using UnityEditor;
using Ludiq;
using Bolt;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lasm.UAlive
{
    public static class UAGUI
    {
        public static void MethodOverride(Metadata method, GUIContent label)
        {
            var _method = ((Method)method.value);
            HUMEditor.Horizontal(() =>
            {
                HUMEditor.Horizontal().Box(HUMEditorColor.DefaultEditorBackground.Darken(0.075f), Styles.borderColor, new RectOffset(2, 2, 2, 2), new RectOffset(1, 1, 1, 1), () =>
                   {
                       if (_method.entry.declaration.isMagic)
                       {
                           GUILayout.Box(GUIContent.none,
                               new GUIStyle()
                               {
                                   normal = new GUIStyleState()
                                   {
                                       background = Images.special_16
                                   }
                               },
                               GUILayout.Width(16), GUILayout.Height(16));
                       }

                       EditorGUILayout.LabelField(GUIContent.none, GUILayout.Width(100));
                       var lastRect = GUILayoutUtility.GetLastRect();

                       HUMEditor.Disabled(_method.entry.declaration.isAbstract, () =>
                       {
                           HUMEditor.Disabled(_method.entry.declaration.hasOptionalOverride, () =>
                           {
                               LudiqGUI.Inspector(method["entry"]["declaration"]["type"], new Rect(lastRect.x, lastRect.y, 80, 20), GUIContent.none);
                           });

                           LudiqGUI.Inspector(method["entry"]["declaration"]["isOverridden"], new Rect(lastRect.x + lastRect.width - 16, lastRect.y, 20, 20), GUIContent.none);

                           GUILayout.Label(label);
                       });

                       HUMEditor.Disabled(_method.entry.declaration.hasOptionalOverride && !_method.entry.declaration.isOverridden, () =>
                       {
                           if (GUILayout.Button("Edit", GUILayout.Width(42)))
                           {
                               GraphWindow.OpenActive(GraphReference.New(_method, true));
                           }
                       });
                   });
            });
        }

        public static void IconFoldout(ref bool isOpen, string label, Texture2D icon, Action content, int padding = 4)
        {
            isOpen = HUMEditor.Foldout(isOpen, new GUIContent(label, icon),
            Styles.backgroundColor.Brighten(0.05f),
            Color.black,
            1,
            () =>
            {
                HUMEditor.Vertical().Box(Styles.backgroundColor, Color.black, new RectOffset(padding, padding, padding, padding), new RectOffset(1, 1, 0, 1), () =>
                {
                    content?.Invoke();
                });
            });
        }

        public static void IconFoldout(ref bool isOpen, string label, Texture2D icon, Action content, Color background, int padding = 4)
        {
            isOpen = HUMEditor.Foldout(isOpen, new GUIContent(label, icon),
            Styles.backgroundColor.Brighten(0.05f),
            Color.black,
            1,
            () =>
            {
                HUMEditor.Vertical().Box(background, Color.black, new RectOffset(padding, padding, padding, padding), new RectOffset(1, 1, 0, 1), () =>
                {
                    content?.Invoke();
                });
            });
        }
    }
}