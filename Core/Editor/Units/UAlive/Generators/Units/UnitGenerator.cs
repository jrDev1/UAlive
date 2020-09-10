using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lasm.UAlive
{
    public abstract class UnitGenerator<TUnit> : Decorator<UnitGenerator<TUnit>, UnitGeneratorAttribute, TUnit> where TUnit : IUnit
    {
        public TUnit unit;

        public UnitGenerator(TUnit unit) { this.unit = unit; }

        public abstract string GenerateValue(ValueInput input);

        public abstract string GenerateValue(ValueOutput output);

        public abstract string GenerateControl(ControlInput input, int indent);
    }
}