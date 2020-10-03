using UnityEditor;

namespace Lasm.UAlive
{
    public sealed class RuntimeTypesInitializer : DeserializedRoutine
    {
        public override void Run()
        {
            var macros = HUMAssets.Find().Assets().OfType<CustomClass>();
            var references = HUMAssets.Find().Assets().OfType<TypeReference>();
            var reference = references.Count == 0 ? null : references[0];

            if (references.Count == 0)
            {
                reference = TypeReference.CreateInstance<TypeReference>();
                HUMIO.Ensure(UAPaths.Generated).Path(); 
                AssetDatabase.CreateAsset(reference, UAPaths.Generated + "TypeReferences.asset");
            }

            RuntimeTypes.instance.references = reference;

            for (int i = 0; i < macros.Count; i++)
            {
                reference.Add(macros[i]);
            }
        }
    }
}
