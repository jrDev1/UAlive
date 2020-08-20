using Ludiq;
using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

namespace Lasm.UAlive
{
    [Generator(typeof(ClassMacro))]
    public class ClassMacroGenerator : TypeMacroGenerator<ClassMacroGenerator, ClassMacro>
    {
        private ClassGenerator @class;
        private NamespaceGenerator @namespace;
        private string output;
        private string guid;

        protected override void BeforeLiveGeneration()
        {
            if (!(string.IsNullOrEmpty(decorated.@namespace) || string.IsNullOrWhiteSpace(decorated.@namespace))) @namespace = NamespaceGenerator.Namespace(decorated.@namespace.ToString());
            @class = ClassGenerator.Class(
                RootAccessModifier.Public,
                ClassModifier.None,
                NoSpace(decorated.title),
                decorated.inherits);
            var att = AttributeGenerator.Attribute<IncludeInSettingsAttribute>().AddParameter(true);
            @class.AddAttribute(att);
            @class.AddInterface(typeof(IUAClass));
            guid = decorated.GetGUID();
        }

        protected override void AfterLiveGeneration()
        {
            //var macroId = PropertyGenerator.Property(AccessModifier.Public, PropertyModifier.Static, typeof(string), "macroID", false).SingleStatementGetter(AccessModifier.Public, guid.As().Code(false));
            var uaClass = FieldGenerator.Field(AccessModifier.Private, FieldModifier.None, typeof(UAClass), "data").CustomDefault("new UAClass(" + guid.As().Code(false) + ");");
            var interfaceUAClass = PropertyGenerator.Property(AccessModifier.Public, PropertyModifier.None, typeof(UAClass), "Class", false).SingleStatementGetter(AccessModifier.Public, "data");
            //@class.AddProperty(macroId);
            @class.AddField(uaClass);
            @class.AddProperty(interfaceUAClass);

            @namespace?.AddClass(@class);
            var usings = CodeBuilder.Using(@class.Usings()) + "\n\n";
            var output = (string.IsNullOrEmpty(decorated.@namespace) || string.IsNullOrWhiteSpace(decorated.@namespace)) ? usings + @class.Generate(0) : usings + @namespace.Generate(0);
            ClassExtensions.Save(guid, decorated, output);
        }

        protected override void DefineCompiledCode()
        {

        }

        protected override void DefineLiveCode()
        {
            var keys = decorated.overrideMethods.KeysToArray();

            // FORCE COMPILE SHELL
            //HUMQuery.For(keys, (list, index) => 
            //{
            //    var nest = decorated.overrideMethods[keys[index]];
            //    if (CanAddMethod(nest))
            //    {
            //        var method = Method(nest.name, nest.scope, nest.modifier, nest.returnType);
            //        AddParameters(method, nest);
            //        @class.AddMethod(method);
            //    }
            //});

            for (int i = 0; i < decorated.overrideMethods.Count; i++)
            { 
                var nest = decorated.overrideMethods[keys[i]];
                if (CanAddMethod(nest))
                {
                    var method = nest.returnType.Is().Void() ? Method(nest.name, nest.scope, nest.modifier, nest) : Method(nest.name, nest.scope, nest.modifier, nest.returnType);
                    if (!nest.returnType.Is().Void()) AddParameters(method, nest);
                    @class.AddMethod(method);
                }
            };
        }

        protected MethodGenerator Method(string key, AccessModifier scope, MethodModifier modifier, Type returnType, string parameters = null)
        {
            var method = MethodGenerator.Method(scope, modifier, returnType, key.Replace(" ", string.Empty));
            var line1 = CodeBuilder.InitializeVariable("val", returnType);
            var line2 = CodeExtensions.Invoke(key, returnType, parameters);
            var line3 = CodeBuilder.NullVoidOrNot(returnType, string.Empty, "\n" + CodeBuilder.Return("val"));
            method.Body(line1 + line2 + line3);
            return method;
        }

        private MethodGenerator Method(string key, AccessModifier scope, MethodModifier modifier, FlowNest nest)
        {
            var method = MethodGenerator.Method(scope, modifier, typeof(Lasm.UAlive.Void), key.Replace(" ", string.Empty));
            method.Body(CodeExtensions.Invoke(key, typeof(Lasm.UAlive.Void), GetParameters(method, nest)));
            return method;
        }

        protected override void AfterCodeGeneration()
        {
            
        }

        protected override void BeforeCodeGeneration()
        {
            
        }

        private bool CanAddMethod(FlowNest nest)
        {
            return nest.isOverridden && nest.hasOptionalOverride || !nest.hasOptionalOverride;
        }

        private void AddParameters(MethodGenerator method, FlowNest nest)
        {
            foreach (KeyValuePair<string, Type> pair in nest.macro.entry.parameters)
            {
                method.AddParameter(ParameterGenerator.Parameter(pair.Key, pair.Value, ParameterModifier.None));
            }
        }

        private string GetParameters(MethodGenerator method, FlowNest nest)
        {
            var parameters = new List<ParameterGenerator>();

            foreach (KeyValuePair<string, Type> pair in nest.macro.entry.parameters)
            {
                parameters.Add(ParameterGenerator.Parameter(pair.Key, pair.Value, ParameterModifier.None));
            }

            return parameters.Count == 0 ? string.Empty : parameters.Parameters();
        }

        private string NoSpace(string str)
        {
            return str.Replace(" ", string.Empty);
        }
    }

    public sealed class UATools : EditorWindow
    {
        public static UATools current;
        [SerializeField]
        private bool compilationIsOpen;
        [SerializeField]
        private bool searchIsOpen;
        [SerializeField]
        private bool explorerIsOpen;

        [MenuItem("Window/UAlive/Tools")]
        public static void Open()
        {
            current = GetWindow<UATools>();
            current.titleContent = new UnityEngine.GUIContent("UA Tools");
        }

        private void OnGUI()
        {
            Images.Cache();

            compilationIsOpen = HUMEditor.Foldout(compilationIsOpen, new GUIContent("Compilation", Images.compilation_16), Styles.backgroundColor, Styles.borderColor, 1, () =>
            {
                HUMEditor.Vertical().Box(Styles.backgroundColor.Brighten(0.06f), Styles.borderColor, new RectOffset(6, 6, 6, 6), new RectOffset(1, 1, 0, 1), () => { LiveStatus(); });
            });

            searchIsOpen = HUMEditor.Foldout(searchIsOpen, new GUIContent("Search", Images.search_16), Styles.backgroundColor, Styles.borderColor, 1, () =>
            {
                EditorGUILayout.HelpBox("The search and replace functionality is not available at this time.", MessageType.Info);
            });

            explorerIsOpen = HUMEditor.Foldout(explorerIsOpen, new GUIContent("Explorer", Images.explorer_16), Styles.backgroundColor, Styles.borderColor, 1, () =>
            {
                EditorGUILayout.HelpBox("The project explorer is not available at this time.", MessageType.Info);
            });
        }

        private void LiveStatus()
        {
            var isLive = false;

            HUMEditor.Horizontal(() =>
            {
                if (EditorPrefs.HasKey("UAlive_Global_IsLive"))
                {
                    isLive = EditorPrefs.GetBool("UAlive_Global_IsLive");
                }

                isLive = GUILayout.Toggle(isLive, "Live");

                EditorPrefs.SetBool("UAlive_Global_IsLive", isLive);

                if (GUILayout.Button("Compile"))
                {

                }
            });
        }
    }
}
