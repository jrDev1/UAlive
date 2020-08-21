using Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    /// <summary>
    /// The root of all Binary Units. Does nothing on its own. Used for consistancy for units and the editors.
    /// </summary>
    [RenamedFrom("Lasm.BoltExtensions.IO.BinarySaveUnit")]
    [RenamedFrom("Lasm.BoltExtensions.BinarySaveUnit")]
    public abstract class BinarySaveUnit : Unit
    {
        protected override void Definition()
        {
            
        }
    }
}