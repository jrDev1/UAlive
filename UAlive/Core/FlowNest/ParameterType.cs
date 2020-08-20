using Ludiq;
using System;

namespace Lasm.UAlive
{
    [Inspectable][Serializable]
    public class ParameterType
    {
        [Serialize]
        public object value = new object();
    }
}