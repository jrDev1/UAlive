using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Ludiq;
using Bolt;
using System.Linq;

namespace Lasm.UAlive
{
    [Editor(typeof(ClassMacro))]
    public sealed class ClassMacroEditor : Inspector
    {
        private ClassMacroGenerator generator;
        private ClassMacro _target;
        private List<SerializedProperty> props = new List<SerializedProperty>();
        private Dictionary<string, Method> tempOverrides = new Dictionary<string, Method>();
        private string focusedControl;
        private string lastTitle;
        private int ticks;
        private Once titleChanged = new Once();
        private AfterTicksCollection<Method> methodTitleChangedCollection = new AfterTicksCollection<Method>();

        public ClassMacroEditor(Metadata metadata) : base(metadata)
        {
        }

        protected override float GetHeight(float width, GUIContent label)
        {
            return 0;
        }

        public override void Initialize()
        {
            Images.Cache();
            _target = metadata.value as ClassMacro;
            generator = ClassMacroGenerator.GetDecorator(_target);
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
            _target.overridesOpen = HUMEditor.Foldout(_target.overridesOpen, new GUIContent("Overrides", Images.override_16),
            Styles.backgroundColor.Brighten(0.05f),
            Color.black,
            1,
            () =>
            {
                HUMEditor.Vertical().Box(Styles.backgroundColor, Color.black, new RectOffset(4, 4, 4, 4), new RectOffset(1, 1, 0, 1), () =>
                {
                    _target.methodOverridesOpen = HUMEditor.Foldout(_target.methodOverridesOpen,
                    new GUIContent("Methods", Images.flow_icon_16),
                    Styles.backgroundColor.Brighten(0.05f),
                    Styles.borderColor,
                    1,
                    () =>
                    {
                        HUMEditor.Vertical().Box(Styles.backgroundColor.Brighten(0.025f), Styles.borderColor, new RectOffset(4, 4, 4, 2), new RectOffset(1, 1, 0, 1), () =>
                        {
                            var overrides = metadata["methods"]["overrides"];

                            tempOverrides.Clear();

                            for (int i = 0; i < overrides.Count; i++)
                            {
                                var nest = ((Method)overrides.ValueMetadata(i).value);
                                if (!nest.isSpecial || (nest.isSpecial && nest.isOverridden))
                                {
                                    var _name = nest.name;
                                    if (position.width - 170 < GUI.skin.label.CalcSize(new GUIContent(_name)).x)
                                    {
                                        if (_name.Length > 11)
                                        {
                                            _name = _name.Remove(11, _name.Length - 11) + "..";
                                        }
                                    }

                                    LudiqGUI.InspectorLayout(overrides.ValueMetadata(i), new GUIContent(_name));
                                }
                                else
                                {
                                    var temp = ((KeyValuePair<string, Method>)overrides[i].value);
                                    tempOverrides.Add(temp.Key, temp.Value);
                                }
                            }

                            GUILayout.Space(6);

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
                                    menu.AddItem(new GUIContent(key), false, (obj) => { tempOverrides[(string)obj].isOverridden = true; _target.Define(); }, key);
                                    if (keys[i] == "OnGUI") startSeparator = true;
                                }

                                menu.ShowAsContext();
                            }
                        });
                    });
                });
            });
        }

        private void Methods(Rect position)
        {
            _target.customMethodsOpen = HUMEditor.Foldout(_target.customMethodsOpen,
            new GUIContent("Methods", Images.flow_icon_16),
            Styles.backgroundColor.Brighten(0.05f),
            Styles.borderColor,
            1,
            () =>
            {
                HUMEditor.Vertical().Box(Styles.backgroundColor.Brighten(0.025f), Styles.borderColor, new RectOffset(4, 4, 4, 2), new RectOffset(1, 1, 0, 1), () =>
                {
                    var methods = metadata["methods"]["custom"];
                    var methodsVal = (List<Method>)metadata["methods"]["custom"].value;

                    for (int i = 0; i < methodsVal.Count; i++)
                    {
                        if (i != 0) GUILayout.Space(2);
                         
                        HUMEditor.Vertical().Box(Styles.backgroundColor.Brighten(0.075f), Styles.borderColor, new RectOffset(4, 4, 4, 2), new RectOffset(1, 1, 1, 1), () =>
                        {
                            var meth = methodsVal[i];
                            meth.name = GUILayout.TextField(meth.name);
                            meth.macro.name = meth.name;

                            HUMEditor.Horizontal(() =>
                            {
                                HUMEditor.Vertical(() =>
                                {
                                    LudiqGUI.InspectorLayout(methods[i], GUIContent.none);
                                });

                                if (GUILayout.Button("-", GUILayout.Width(16), GUILayout.Height(18)))
                                {
                                    methodsVal.Remove(meth);
                                    AssetDatabase.RemoveObjectFromAsset(meth.macro);
                                    AssetDatabase.SaveAssets();
                                    AssetDatabase.Refresh();
                                    _target.Define();
                                }
                            });
                        });
                    }

                    if (GUILayout.Button("+ New Method"))
                    {
                        var meth = new Method();
                        meth.Initialize();
                        meth.showLabel = false;
                        meth.name = string.Empty;
                        meth.macro.hideFlags = HideFlags.HideInHierarchy;
                        AssetDatabase.AddObjectToAsset(meth.macro, _target);
                        methodsVal.Add(meth);
                        _target.Define();
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
                });
            });
        }

        private void Variables(Rect position)
        {
            _target.customVariablesOpen = HUMEditor.Foldout(_target.customVariablesOpen,
            new GUIContent("Variables", Images.variables_16),
            Styles.backgroundColor.Brighten(0.05f),
            Styles.borderColor,
            1,
            () =>
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
                                        LudiqGUI.InspectorLayout(variable["type"], GUIContent.none);
                                    });

                                    if (GUILayout.Button("-", GUILayout.Width(16), GUILayout.Height(14)))
                                    {
                                        variablesVal.variables.Remove(variableVal);
                                        AssetDatabase.RemoveObjectFromAsset(variableVal.getter.macro);
                                        AssetDatabase.RemoveObjectFromAsset(variableVal.setter.macro);
                                        AssetDatabase.SaveAssets();
                                        AssetDatabase.Refresh();
                                        _target.Define();
                                    } 
                                });

                                GUILayout.Space(2);

                                HUMEditor.Vertical(() =>
                                {
                                    BeginBlock(metadata, position, GUIContent.none);
                                    LudiqGUI.InspectorLayout(variable["value"].Cast(variableVal.type), GUIContent.none);
                                    if (EndBlock(metadata))
                                    {
                                        variableVal.Changed();
                                        _target.Define();
                                    }
                                });
                            });

                            GUILayout.Space(2);
                        });
                    }

                    BeginBlock(metadata, position, GUIContent.none);
                    if (GUILayout.Button("+ New Variable"))
                    {
                        var variable = new Variable();
                        variable.getter.Initialize();
                        variable.setter.Initialize();
                        variable.getter.showLabel = false;
                        variable.setter.showLabel = false;
                        variable.getter.name = string.Empty;
                        variable.setter.name = string.Empty;
                        variable.getter.macro.hideFlags = HideFlags.HideInHierarchy;
                        variable.setter.macro.hideFlags = HideFlags.HideInHierarchy;
                        AssetDatabase.AddObjectToAsset(variable.getter.macro, _target);
                        AssetDatabase.AddObjectToAsset(variable.setter.macro, _target);
                        variablesVal.variables.Add(variable);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
                    if (EndBlock(metadata))
                    {
                        _target.Define();
                    }
                });
            });
        }
    }
}