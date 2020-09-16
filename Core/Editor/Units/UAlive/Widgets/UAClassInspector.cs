using Ludiq;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    [Inspector(typeof(UAClass))]
    public sealed class UAClassInspector : Inspector
    {
        [SerializeField]
        private Rect position;

        protected override bool cacheHeight => false;

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

                var _type = variable.reference.declaration.type;

                if (_type.BaseType != null || (_type.BaseType == null && variable.value != null))
                {
                    var castedMeta = _type.BaseType == null ? childProperty[i]["value"].Cast(variable.value.GetType()) : childProperty[i]["value"].Cast(variable.reference.declaration.type);
                    var thisHeight = variable.value == null ? 18 : LudiqGUI.GetInspectorHeight(childProperty[i].Inspector(), castedMeta, position.width, new GUIContent(variable.reference.name.Prettify())) + 2;
                    LudiqGUI.Inspector(castedMeta, new Rect(position.x, position.y + this.position.height, position.width, thisHeight), new GUIContent(variable.reference.name.Prettify()));
                    childProperty[i]["backingValue"].value = childProperty[i]["value"].value;
                    this.position.height += thisHeight;
                }
                else
                {
                    EditorGUI.LabelField(new Rect(position.x, position.y + this.position.height, position.width, 14), variable.reference.name.Prettify());
                    this.position.height += 18;
                }
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
                var _type = variable?.reference?.declaration?.type;

                if (_type != null)
                {
                    if (_type.BaseType != null || (_type.BaseType == null && variable.value != null))
                    {
                        var thisHeight = variable.value == null ? 18 : LudiqGUI.GetInspectorHeight(childProperty[i].Inspector(), childProperty[i]["value"].Cast(variable.reference.declaration.type), position.width, new GUIContent(variable.reference.name.Prettify())) + 2;
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
    }
}
