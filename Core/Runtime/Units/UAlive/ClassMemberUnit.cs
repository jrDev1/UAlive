using Bolt;
using Lasm.OdinSerializer;
using Ludiq;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace Lasm.UAlive
{
    [Serializable]
    public abstract class ClassMemberUnit : Unit
    {  
        [DoNotSerialize]
        [AllowsNull]
        public ValueInput target;
        [Serialize]
        public CustomClass Class;

        protected override void Definition()
        {
            target = ValueInput<IUAClass>("target").AllowsNull();
        }
    }
}