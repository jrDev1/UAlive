using UnityEngine;
using Ludiq;
using System;

namespace Lasm.UAlive
{
    [Inspector(typeof(Variable))]
    public sealed class VariableInspector : Inspector
    {
        private Metadata metaValue => metadata["value"];
        private Metadata metaType => metadata["type"];
        private Metadata metaName => metadata["name"];
        private int padding => 4;
        private float height;

        public VariableInspector(Metadata metadata) : base(metadata)
        {
        }

        protected override float GetHeight(float width, GUIContent label)
        {
            return 28 + metaValue.Inspector().GetCachedHeight(width - 8, GUIContent.none, metadata.Inspector()) - 18;
        }

        protected override void OnGUI(Rect position, GUIContent label)
        {
            height = metaValue.Inspector().GetCachedHeight(position.width - 8, GUIContent.none, metadata.Inspector());
            Block(position, () =>
            {
                var half = new Rect(position.x, position.y + 4, position.width / 2, 18);
                var withPadding = half.Subtract().Width(padding*2);
                var nameRect = withPadding.Add().X(padding).Add().Width(20);
                var typeRect = withPadding.Add().X((padding * 2) + nameRect.width).Subtract().Width(20);
                var valueRect = new Rect(position.x + 4, position.y + withPadding.height + 8, position.width - 8, height);
                metaName.value = GUI.TextField(nameRect, (string)metaName.value);
                LudiqGUI.Inspector(metaType, typeRect, GUIContent.none);
                LudiqGUI.Inspector(metaValue.Cast((Type)metaType.value), valueRect, GUIContent.none);
            });
        }

        public void Block(Rect position, Action action)
        {
            BeginBlock(metadata, position, GUIContent.none);
            action?.Invoke();
            if (EndBlock(metadata)) metadata.RecordUndo();
        }
    }
}
