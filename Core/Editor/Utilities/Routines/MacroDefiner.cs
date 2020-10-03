namespace Lasm.UAlive
{
    public sealed class MacroDefiner : DeserializedRoutine
    {
        public override void Run()
        {
            var macros = HUMAssets.Find().Assets().OfType<CustomClass>();

            for (int i = 0; i < macros.Count; i++)
            {
                macros[i].Definer().Define();
            }
        }
    }
}
