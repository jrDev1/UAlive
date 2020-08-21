using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lasm.UAlive
{
    public abstract class UnitCodeGenerator<T> : Decorator<UnitCodeGenerator<T>, CodeGenAttribute, T> where T : IUnit
    {
        public T unit;

        public UnitCodeGenerator(T unit) { this.unit = unit; }

        public abstract string GenerateValue(ValueInput input);

        public abstract string GenerateValue(ValueOutput output);

        public abstract string GenerateControl(ControlInput input, int indent);
    }
}