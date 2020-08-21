using Ludiq;
using System;

namespace Lasm.UAlive
{
    [Serializable][Inspectable]
    public class EnumItem
    {
        [Inspectable]
        public int index;
        [Inspectable]
        public string name;
    }
}