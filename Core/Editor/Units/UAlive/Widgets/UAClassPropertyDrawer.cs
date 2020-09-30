using Ludiq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    [CustomPropertyDrawer(typeof(UAClass))]
    public sealed class UAClassPropertyDrawer : PropertyDrawer
    {
        [SerializeField]
        private Rect position;
        private CustomClass @class;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var childProperty = property.FindPropertyRelative("variables").Copy();
            Metadata variablesMeta = Metadata.FromProperty(childProperty);
            var _pos = 0f;

            for (int i = 0; i < variablesMeta.Count; i++)
            {
                var variable = ((RuntimeVariable)variablesMeta[i].value);

                if (variable.reference != null)
                {
                    var _type = variable.reference.declaration.type;

                    if (_type.BaseType != null || (_type.BaseType == null && variable.value != null))
                    {
                        var thisHeight = variable.value == null ? 20 : LudiqGUI.GetInspectorHeight(variablesMeta[i].Inspector(), variablesMeta[i]["value"].Cast(variable.reference.declaration.type), position.width, new GUIContent(variable.reference.name.Prettify())) + 2;
                        _pos += thisHeight - (_type.BaseType == null ? 20 : 0);
                    }
                    else
                    {
                        _pos += 18;
                    }
                }
            }
            return _pos;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, GUIContent.none, property);
            this.position.width = position.width;
            this.position.height = 0;

            var Class = fieldInfo.GetValue(property.serializedObject.targetObject) as UAClass;
            Class.EnsureInitialized(Class.GUID);
            Class.Refresh(Class.macro);

            var childProperty = property.FindPropertyRelative("variables").Copy();
            Metadata variablesMeta = Metadata.FromProperty(childProperty);

            if (@class == null) @class = property.FindPropertyRelative("macro").objectReferenceValue as CustomClass;

            for (int i = 0; i < variablesMeta.Count; i++)
            {
                var variable = ((RuntimeVariable)variablesMeta[i].value);
                if (variable.reference != null)
                {
                    if ((variable.value == null && variable.reference.declaration.type != typeof(UnityEngine.Object)) || variable.reference.declaration.type != variable.value.GetType())
                    {
                        variable.value = variable.reference.declaration.defaultValue;
                    }

                    var _type = variable.reference.declaration.type;

                    if (_type.BaseType != null || (_type.BaseType == null && variable.value != null))
                    {
                        var castedMeta = _type.BaseType == null ? variablesMeta[i]["value"].Cast(variable.value.GetType()) : variablesMeta[i]["value"].Cast(variable.reference.declaration.type);
                        var thisHeight = variable.value == null ? 20 : LudiqGUI.GetInspectorHeight(variablesMeta[i].Inspector(), castedMeta, position.width, new GUIContent(variable.reference.name.Prettify())) + 2;
                        var objPos = new Rect(position.x, position.y + this.position.height, position.width, thisHeight);
                        if (@class != null && _type.Inherits<UnityEngine.Object>() && @class.inheritance.type.Inherits<Component>())
                        {
                            variable.value = EditorGUI.ObjectField(objPos.Subtract().Height(2f), variable.reference.name.Prettify(), variable.value as UnityEngine.Object, _type, true);
                        }
                        else
                        {
                            LudiqGUI.Inspector(castedMeta, objPos, new GUIContent(variable.reference.name.Prettify()));
                        }
                        variablesMeta[i]["backingValue"].value = variablesMeta[i]["value"].value;
                        this.position.height += thisHeight;
                    }
                    else
                    {
                        EditorGUI.LabelField(new Rect(position.x, position.y + this.position.height, position.width, 14), variable.reference.name.Prettify());
                        this.position.height += 18;
                    }
                }
            } 

            EditorGUI.EndProperty();
        }
    }
}
