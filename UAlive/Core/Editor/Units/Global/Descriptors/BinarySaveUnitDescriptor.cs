using Ludiq;
using Bolt;
using UnityEngine;
using UnityEditor;

namespace Lasm.UAlive.IO
{
    /// <summary>
    /// A descriptor for all BinarySaveUnits. Provides the fetching and application of the icon for these units.
    /// </summary>
    [Descriptor(typeof(BinarySaveUnit))]
    public class BinarySaveUnitDescriptor : UnitDescriptor<BinarySaveUnit>
    {
        public BinarySaveUnitDescriptor(BinarySaveUnit target) : base(target)
        {
        }

        protected override EditorTexture DefinedIcon()
        {
            Images.Cache();
            return EditorTexture.Single(Images.binary_save_32);
        }

        protected override EditorTexture DefaultIcon()
        {
            Images.Cache();
            return EditorTexture.Single(Images.binary_save_32);
        }
    }
}