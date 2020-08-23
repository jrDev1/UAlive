using UnityEditor;

namespace Lasm.UAlive
{
    public sealed class MenuCommands
    {
        [MenuItem("Tools/UAlive/Generate Live Code")]
        public static void GenerateLive()
        {
            var assets = HUMAssets.Find().Assets().OfType<ClassMacro>();
            for (int i = 0; i < assets.Count; i++)
            {
                ClassMacroGenerator.GetDecorator(assets[i]).GenerateLiveCode();
            }

            var enumAssets = HUMAssets.Find().Assets().OfType<EnumMacro>();
            for (int i = 0; i < enumAssets.Count; i++)
            {
                EnumMacroGenerator.GetDecorator(enumAssets[i]).GenerateLiveCode();
            }

            var interfaceAssets = HUMAssets.Find().Assets().OfType<InterfaceMacro>();
            for (int i = 0; i < interfaceAssets.Count; i++)
            {
                InterfaceMacroGenerator.GetDecorator(interfaceAssets[i]).GenerateLiveCode();
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}