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
                HUMEditor.Horizontal().Box(HUMEditorColor.DefaultEditorBackground, Styles.borderColor, new RectOffset(2, 2, 2, 2), new RectOffset(1, 1, 1, 1), () =>
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

        public static void MethodCustom(Metadata method, GUIContent label)
        {
            var _method = ((Method)method.value);
            HUMEditor.Horizontal(() =>
            {
                HUMEditor.Horizontal().Box(HUMEditorColor.DefaultEditorBackground, Styles.borderColor, new RectOffset(0, 0, 0, 0), new RectOffset(1, 1, 1, 1), () =>
                      {
                          EditorGUI.BeginDisabledGroup(_method.entry.declaration.hasOptionalOverride);
                          EditorGUILayout.LabelField(GUIContent.none);
                          var lastRect = GUILayoutUtility.GetLastRect();
                          LudiqGUI.Inspector(method["entry"]["declaration"]["type"], new Rect(lastRect.x, lastRect.y, lastRect.width, 20), GUIContent.none);
                          EditorGUI.EndDisabledGroup();

                          if (GUILayout.Button("Edit", GUILayout.Width(42)))
                          {
                              GraphWindow.OpenActive(GraphReference.New(_method, true));
                          }
                      });
            });
        }
    }
}