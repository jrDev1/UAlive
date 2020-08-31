using UnityEditor;

namespace Lasm.UAlive
{
    public sealed class MenuCommands
    {
        [MenuItem("Tools/UAlive/Generate Live Code")]
        public static void GenerateLive()
        {
            var assets = HUMAssets.Find().Assets().OfType<CustomClass>();
            for (int i = 0; i < assets.Count; i++)
            {
                CustomClassGenerator.GetDecorator(assets[i]).GenerateLiveCode();
            }

            var enumAssets = HUMAssets.Find().Assets().OfType<CustomEnum>();
            for (int i = 0; i < enumAssets.Count; i++)
            {
                CustomEnumGenerator.GetDecorator(enumAssets[i]).GenerateLiveCode();
            }

            var interfaceAssets = HUMAssets.Find().Assets().OfType<CustomInterface>();
            for (int i = 0; i < interfaceAssets.Count; i++)
            {
                CustomInterfaceGenerator.GetDecorator(interfaceAssets[i]).GenerateLiveCode();
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Create/UAlive/Custom Class", priority = 0)]
        public static void CreateClass()
        {
            CustomClass @class = null;
            @class = TypeExtensions.CreateAsset<CustomClass>();
        }

        [MenuItem("Assets/Create/UAlive/Custom Enum", priority = 0)]
        private static void CreateEnum()
        {
            CustomEnum @enum = null;
            @enum = TypeExtensions.CreateAsset<CustomEnum>();
        }

        [MenuItem("Assets/Create/UAlive/Custom Interface", priority = 0)]
        public static void CreateInterface()
        {
            CustomInterface @interface = null;
            @interface = TypeExtensions.CreateAsset<CustomInterface>();
        }

    }
}