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
        public ClassMacro macro;
        [Serialize]
        public string memberName;
        [Serialize]
        public int id = 0;

        protected override void Definition()
        {
            target = ValueInput<IUAClass>("target").AllowsNull();
        }
    }
}