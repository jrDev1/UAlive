﻿using System;

namespace Lasm.UAlive
{
    public static class RuntimeClassExtensions
    {
        public static void Invoke(IUAClass @class, string name, Action<object> returnMethod, bool isOverride = false, params object[] parameters)
        {
            if (isOverride)
            {
                @class.Class.macro.methods.overrides.current[name].Invoke(@class, returnMethod, parameters);
                return;
            }

            for (int i = 0; i < @class.Class.macro.methods.custom.Count; i++)
            {
                var instance = @class.Class.macro.methods.custom[i];
                if (instance.name == name)
                {
                    instance.Invoke(@class, returnMethod, parameters);
                    return;
                }
            }
        }

        public static void GetClass(ref CustomClass @class, string guid)
        {
            @class = (@class ?? RuntimeTypes.instance.references.Get(guid)) as CustomClass;
        }
    }
}
