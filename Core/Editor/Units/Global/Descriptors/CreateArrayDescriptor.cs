using UnityEngine;
using UnityEditor;
using Ludiq;
using Bolt;

namespace Lasm.UAlive
{
    /// <summary>
    /// The descriptor that sets the icon for CreateMultiArray.
    /// </summary>
    [Descriptor(typeof(CreateMultiArray))]
    [RenamedFrom("Lasm.BoltExtensions.CreateArrayDescriptor")]
    public class CreateMultiArrayDescriptor : UnitDescriptor<CreateMultiArray>
    {
        public CreateMultiArrayDescriptor(CreateMultiArray unit) : base(unit)
        {

        }

        /// <summary>
        /// Sets the default icon of CreateMultiArray.
        /// </summary>
        protected override EditorTexture DefaultIcon()
        {
            Images.Cache();
            return EditorTexture.Single(Images.multi_array_32);
        }

        /// <summary>
        /// Sets the defined icon of CreateMultiArray.
        /// </summary>
        protected override EditorTexture DefinedIcon()
        {
            Images.Cache();
            return EditorTexture.Single(Images.multi_array_32);
        }
    }
}