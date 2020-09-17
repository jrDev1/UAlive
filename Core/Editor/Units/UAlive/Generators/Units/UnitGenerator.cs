using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lasm.UAlive
{
    [UnitGenerator(typeof(Unit))]
    public class UnitGenerator : Decorator<UnitGenerator, UnitGeneratorAttribute, Unit>
    {
        public Unit unit;

        public UnitGenerator(Unit unit) { this.unit = unit; }

        public virtual string GenerateValue(ValueInput input) { return string.Empty; }

        public virtual string GenerateValue(ValueOutput output) { return string.Empty; }

        public virtual string GenerateControl(ControlInput input, int indent) { return string.Empty; }
    }

    public class UnitGenerator<TUnit> : UnitGenerator where TUnit : Unit
    {
        public TUnit Unit;

        public UnitGenerator(Unit unit) : base (unit) { this.unit = unit; Unit = unit as TUnit; }
    }
}