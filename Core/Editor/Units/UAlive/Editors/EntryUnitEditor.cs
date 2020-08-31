using UnityEngine;
using Ludiq;
using Bolt;

namespace Lasm.UAlive
{
    [Editor(typeof(EntryUnit))]
    public sealed class EntryUnitEditor : UnitEditor
    {
        public EntryUnitEditor(Metadata metadata) : base(metadata)
        {
        }

        protected override float GetInspectorHeight(float width)
        {
            if (!((EntryUnit)metadata.value).declaration.hasOptionalOverride) return base.GetInspectorHeight(width);
            return 0;
        }

        protected override void OnInspectorGUI(Rect position)
        {
            BeginBlock(metadata, position, GUIContent.none);
            if (!((EntryUnit)metadata.value).declaration.hasOptionalOverride) base.OnInspectorGUI(position);
            if (EndBlock(metadata))
            {
                ((EntryUnit)unit).Define();
            }
        }
    }
}