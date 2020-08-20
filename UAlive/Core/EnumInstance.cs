using Ludiq;
using System;

namespace Lasm.UAlive
{
    [Serializable][Inspectable]
    public sealed class EnumInstance
    {
        [Inspectable]
        public EnumMacro macro;
        [Inspectable]
        public EnumItem item;
    }
}