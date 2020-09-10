using Ludiq;
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

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var childProperty = property.FindPropertyRelative("variables").Copy();
            Metadata variablesMeta = Metadata.FromProperty(childProperty);
            var _pos = 0f;

            for (int i = 0; i < variablesMeta.Count; i++)
            {
                var variable = ((RuntimeVariable)variablesMeta[i].value);
                var thisHeight = variable.value == null ? 20 : LudiqGUI.GetInspectorHeight(variablesMeta[i].Inspector(), variablesMeta[i]["value"].Cast(variable.reference.declaration.type), position.width, new GUIContent(variable.reference.name.Prettify())) + 2;
                _pos += thisHeight;
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

            for (int i = 0; i < variablesMeta.Count; i++)
            {
                var variable = ((RuntimeVariable)variablesMeta[i].value);
                if ((variable.value == null && variable.reference.declaration.type != typeof(UnityEngine.Object)) || variable.reference.declaration.type != variable.value.GetType())
                {
                    variable.value = variable.reference.declaration.defaultValue;
                }
                var thisHeight = variable.value == null ? 20 : LudiqGUI.GetInspectorHeight(variablesMeta[i].Inspector(), variablesMeta[i]["value"].Cast(variable.reference.declaration.type), position.width, new GUIContent(variable.reference.name.Prettify())) + 2;
                LudiqGUI.Inspector(variablesMeta[i]["value"].Cast(variable.reference.declaration.type), new Rect(position.x, position.y + this.position.height, position.width, thisHeight), new GUIContent(variable.reference.name.Prettify()));
                variablesMeta[i]["backingValue"].value = variablesMeta[i]["value"].value;
                this.position.height += thisHeight;
            } 

            EditorGUI.EndProperty();
        }
    }

    [Inspector(typeof(UAClass))]
    public sealed class UAClassInspector : Inspector
    {
        [SerializeField]
        private Rect position;

        public UAClassInspector(Metadata metadata) : base(metadata)
        {
        }

        protected override void OnGUI(Rect position, GUIContent label)
        {
            BeginBlock(metadata, position, GUIContent.none);
            this.position.width = position.width;
            this.position.height = 0;

            var Class = metadata.value as UAClass;
            Class.EnsureInitialized(Class.GUID);
            Class.Refresh(Class.macro);

            var childProperty = metadata["variables"];

            for (int i = 0; i < childProperty.Count; i++)
            {
                var variable = ((RuntimeVariable)childProperty[i].value);
                if ((variable.value == null && variable.reference.declaration.type != typeof(UnityEngine.Object)) || variable.reference.declaration.type != variable.value.GetType())
                {
                    variable.value = variable.reference.declaration.defaultValue;
                }
                var thisHeight = variable.value == null ? 20 : LudiqGUI.GetInspectorHeight(childProperty[i].Inspector(), childProperty[i]["value"].Cast(variable.reference.declaration.type), position.width, new GUIContent(variable.reference.name.Prettify())) + 2;
                LudiqGUI.Inspector(childProperty[i]["value"].Cast(variable.reference.declaration.type), new Rect(position.x, position.y + this.position.height, position.width, thisHeight), new GUIContent(variable.reference.name.Prettify()));
                childProperty[i]["backingValue"].value = childProperty[i]["value"].value;
                this.position.height += thisHeight;
            }

            if (EndBlock(metadata))
            {
                metadata.RecordUndo();
            }
        }

        protected override float GetHeight(float width, GUIContent label)
        {
            var childProperty = metadata["variables"];
            var _pos = 0f;

            for (int i = 0; i < childProperty.Count; i++)
            {
                var variable = ((RuntimeVariable)childProperty[i].value);
                var thisHeight = variable.value == null ? 20 : LudiqGUI.GetInspectorHeight(childProperty[i].Inspector(), childProperty[i]["value"].Cast(variable.reference.declaration.type), position.width, new GUIContent(variable.reference.name.Prettify())) + 2;
                _pos += thisHeight;
            }
            return _pos;
        }
    }
}
