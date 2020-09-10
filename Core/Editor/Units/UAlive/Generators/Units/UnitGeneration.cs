using Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    public static class UnitGeneration
    {
        public static string GenerateValue<T>(this T unit, ValueInput input) where T : IUnit
        {
            return UnitGenerator<T>.GetDecorator(unit).GenerateValue(input);
        }

        public static string GenerateValue<T>(this T unit, ValueOutput output) where T : IUnit
        {
            return UnitGenerator<T>.GetDecorator(unit).GenerateValue(output);
        }

        public static string GenerateControl<T>(this T unit, ControlInput input, int indent) where T : IUnit
        {
            return UnitGenerator<T>.GetDecorator(unit).GenerateControl(input, indent);
        }
    }
}