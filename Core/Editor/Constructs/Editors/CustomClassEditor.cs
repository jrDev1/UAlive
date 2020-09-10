using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Ludiq;
using Bolt;
using System.Linq;
using System;

namespace Lasm.UAlive
{
    [Editor(typeof(CustomClass))]
    public sealed class CustomClassEditor : Inspector
    {
        private CustomClassGenerator generator;
        private CustomClass _target;
        private Dictionary<string, Method> tempOverrides = new Dictionary<string, Method>();

        public CustomClassEditor(Metadata metadata) : base(metadata)
        {
        }

        protected override float GetHeight(float width, GUIContent label)
        {
            return 0;
        }

        public override void Initialize()
        {
            Images.Cache();
            _target = metadata.value as CustomClass;
            generator = CustomClassGenerator.GetDecorator(_target);
        }

        protected override void OnGUI(Rect position, GUIContent label)
        {
            BeginBlock(metadata, position, GUIContent.none);

            Declaration(position);

            EditorGUILayout.Space(8);

            Variables(position);

            EditorGUILayout.Space(8);

            Methods(position);

            EditorGUILayout.Space(8);

            Overrides(position);

            if (EndBlock(metadata))
            {
                metadata.RecordUndo();
                CSharpPreview.instance?.Changed();
            }
        }

        private void Declaration(Rect position)
        {
            HUMEditor.Vertical().Box(Styles.backgroundColor.Brighten(0.05f), Color.black, new RectOffset(4, 4, 4, 4), new RectOffset(1, 1, 1, 1), () =>
            {
                _target.title = EditorGUILayout.TextField(new GUIContent("Class Name"), _target.title);
                _target.@namespace = EditorGUILayout.TextField(new GUIContent("Namespace"), _target.@namespace);

                BeginBlock(metadata, position, GUIContent.none);
                LudiqGUI.InspectorLayout(metadata["inheritance"]["type"]);
                if (EndBlock(metadata))
                {
                    _target.Define();
                }
            });
        }

        private void Overrides(Rect position)
        {
            UAGUI.IconFoldout(ref _target.editorData.overridesOpen, "Overrides", Images.override_16, () =>
            {
                UAGUI.IconFoldout(ref _target.editorData.methodOverridesOpen, "Methods", Images.flow_icon_16, () =>
                {
                    HUMEditor.Vertical().Box(Styles.backgroundColor.Brighten(0.025f), Styles.borderColor, new RectOffset(4, 4, 4, 2), new RectOffset(1, 1, 0, 1), () =>
                    {
                        var overrides = metadata["methods"]["overrides"]["current"];
                        tempOverrides.Clear();

                        for (int i = 0; i < overrides.Count; i++)
                        {
                            var method = ((Method)overrides.ValueMetadata(i).value);
                            if (!method.entry.declaration.isMagic || (method.entry.declaration.isMagic && method.entry.declaration.isOverridden))
                            {
                                var _name = method.name;
                                if (position.width - 170 < GUI.skin.label.CalcSize(new GUIContent(_name)).x)
                                {
                                    if (_name.Length > 11)
                                    {
                                        _name = _name.Remove(11, _name.Length - 11) + "..";
                                    }
                                }

                                UAGUI.MethodOverride(overrides.ValueMetadata(i), new GUIContent(_name));
                                if (i < overrides.Count - 1) EditorGUILayout.Space(2);
                            }
                            else
                            {
                                var temp = ((KeyValuePair<string, Method>)overrides[i].value);
                                tempOverrides.Add(temp.Key, temp.Value);
                            }
                        }

                        GUILayout.Space(2);

                        if (GUILayout.Button("+ Message"))
                        {
                            GenericMenu menu = new GenericMenu();
                            var keys = tempOverrides.KeysToList();
                            var startSeparator = false;
                            for (int i = 0; i < keys.Count; i++)
                            {
                                if (startSeparator)
                                {
                                    menu.AddSeparator("");
                                    startSeparator = false;
                                }
                                var key = keys[i];
                                menu.AddItem(new GUIContent(key), false, (obj) => { tempOverrides[(string)obj].entry.declaration.isOverridden = true; }, key);
                                if (keys[i] == "OnGUI") startSeparator = true;
                            }

                            menu.ShowAsContext();
                        }
                    });
                });
            });
        }

        private void Methods(Rect position)
        {
            UAGUI.IconFoldout(ref _target.editorData.customMethodsOpen, "Methods", Images.flow_icon_16, () =>
            {
                HUMEditor.Vertical().Box(Styles.backgroundColor.Brighten(0.025f), Styles.borderColor, new RectOffset(4, 4, 4, 2), new RectOffset(1, 1, 0, 1), () =>
                {
                    var methods = metadata["methods"]["custom"];
                    var methodsVal = (List<Method>)metadata["methods"]["custom"].value;

                    for (int i = 0; i < methodsVal.Count; i++)
                    {
                        if (i != 0) GUILayout.Space(2);
                        var meth = methodsVal[i];

                        methodsVal[i].isOpen = HUMEditor.Foldout(methodsVal[i].isOpen, Styles.backgroundColor.Brighten(0.05f), Styles.borderColor, 1,
                        () => 
                        {
                            meth.name = GUILayout.TextField(meth.name);
                            meth.entry.declaration.name = meth.name;

                            if (GUILayout.Button("Edit", GUILayout.Width(42)))
                            {
                                GraphWindow.OpenActive(GraphReference.New(methodsVal[i], true));
                            }

                            if (GUILayout.Button("-", GUILayout.Width(16), GUILayout.Height(18)))
                            {
                                methodsVal.Remove(meth);
                                meth.entry.Define();
                                AssetDatabase.RemoveObjectFromAsset(meth);
                                AssetDatabase.SaveAssets();
                                AssetDatabase.Refresh();
                                _target.Define();
                            }
                        },
                        () => 
                        {
                            HUMEditor.Vertical(() =>
                            {
                                BeginBlock(methods[i], position, GUIContent.none);
                                HUMEditor.Horizontal(() =>
                                {
                                    HUMEditor.Vertical().Box(HUMEditorColor.DefaultEditorBackground.Darken(0.025f), Styles.borderColor, new RectOffset(8,8,8,8), new RectOffset(1, 1, 0, 1), () =>
                                    {
                                        LudiqGUI.InspectorLayout(methods[i]["entry"]["declaration"]["type"], new GUIContent("Returns"));
                                        LudiqGUI.InspectorLayout(methods[i]["entry"]["declaration"]["pure"], new GUIContent("Pure"));
                                        UAGUI.IconFoldout(ref methodsVal[i].entry.declaration.parametersOpen, "Parameters", Images.parameters_16, () =>
                                        {
                                            LudiqGUI.InspectorLayout(methods[i]["entry"]["declaration"]["parameters"], GUIContent.none);
                                        }, Styles.backgroundColor.Brighten(0.05f),0);
                                    });
                                });
                                if (EndBlock(methods[i])) meth.entry.Define();
                            });
                        });
                    }

                    if (GUILayout.Button("+ New Method"))
                    {
                        var meth = Method.Create(_target);
                        methodsVal.Add(meth);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        _target.Define();
                    }
                });
            });
        }

        private void Variables(Rect position)
        {
            UAGUI.IconFoldout(ref _target.editorData.customVariablesOpen, "Variables", Images.variables_16, ()=> 
            {
                HUMEditor.Vertical().Box(Styles.backgroundColor.Brighten(0.025f), Styles.borderColor, new RectOffset(4, 4, 4, 2), new RectOffset(1, 1, 0, 1), () =>
                {
                    var variables = metadata["variables"];
                    var variablesVal = (Variables)metadata["variables"].value;

                    for (int i = 0; i < variablesVal.variables.Count; i++)
                    {
                        if (i != 0) GUILayout.Space(2);

                        HUMEditor.Vertical().Box(Styles.backgroundColor.Brighten(0.075f), Styles.borderColor, new RectOffset(4, 4, 4, 2), new RectOffset(1, 1, 1, 1), () =>
                        {
                            var variable = variables["variables"][i];
                            var variableVal = (Variable)variable.value;

                            HUMEditor.Vertical(() =>
                            {
                                HUMEditor.Horizontal(() =>
                                {
                                    HUMEditor.Vertical(() =>
                                    {
                                        LudiqGUI.InspectorLayout(variable["name"], GUIContent.none);
                                    });

                                    GUILayout.Label(GUIContent.none, new GUIStyle() { fixedWidth = 4 });

                                    HUMEditor.Vertical(() =>
                                    {
                                        LudiqGUI.InspectorLayout(variable["declaration"]["type"], GUIContent.none);
                                    });

                                    if (GUILayout.Button("-", GUILayout.Width(16), GUILayout.Height(14)))
                                    {
                                        variablesVal.variables.Remove(variableVal);
                                        variableVal.declaration.Changed();
                                        AssetDatabase.RemoveObjectFromAsset(variableVal.getter);
                                        AssetDatabase.RemoveObjectFromAsset(variableVal.setter);
                                        AssetDatabase.RemoveObjectFromAsset(variableVal);
                                        AssetDatabase.SaveAssets();
                                        AssetDatabase.Refresh();
                                        _target.Define();
                                    }
                                });

                                GUILayout.Space(2);

                                HUMEditor.Vertical(() =>
                                {
                                    LudiqGUI.InspectorLayout(variable["declaration"]["defaultValue"].Cast(variableVal.declaration.type), GUIContent.none);
                                });
                            });

                            GUILayout.Space(2);
                        });
                    }

                    if (GUILayout.Button("+ New Variable"))
                    {
                        var variable = Variable.Create(_target);
                        AssetDatabase.AddObjectToAsset(variable, _target);
                        variablesVal.variables.Add(variable);
                        _target.Define();
                    }
                });
            });
        }
    }
}