using Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    public static class UnitGeneration
    {
        public static string GenerateValue<T>(this T unit, ValueInput input) where T : Unit
        {
            return UnitGenerator<T>.GetDecorator(unit, unit).GenerateValue(input);
        }

        public static string GenerateValue<T>(this T unit, ValueOutput output) where T : Unit
        {
            return UnitGenerator<T>.GetDecorator(unit, unit).GenerateValue(output);
        }

        public static string GenerateControl<T>(this T unit, ControlInput input, ControlGenerationData data, int indent) where T : Unit
        {
            return UnitGenerator<T>.GetDecorator(unit, unit).GenerateControl(input, data, indent);
        }
    }
}