using UnityEngine;
using Ludiq;
using System;

namespace Lasm.UAlive
{
    [Inspector(typeof(EnumItem))]
    public sealed class EnumItemInspector : Inspector
    {
        private Metadata metaIndex => metadata["index"];
        private Metadata metaName => metadata["name"];

        public EnumItemInspector(Metadata metadata) : base(metadata)
        {
        }

        protected override float GetHeight(float width, GUIContent label)
        {
            return 26;
        }

        protected override void OnGUI(Rect position, GUIContent label)
        {
            Block(position, () =>
            {
                var indexRect = new Rect(position.x, position.y + 4, 32, 18);
                var nameRect = indexRect.Set().Width(position.width - indexRect.width - 4).Set().X(indexRect.x + indexRect.width + 4);
                metaName.value = GUI.TextField(nameRect, (string)metaName.value);
                LudiqGUI.Inspector(metaIndex, indexRect, GUIContent.none);
                LudiqGUI.Inspector(metaName, nameRect, GUIContent.none);
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
