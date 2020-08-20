namespace Lasm.UAlive
{
    public static partial class UAPaths
        {
            public static string Root => HUMIO.PathOf("ua_root");

            public static string GeneratedRoot => HUMIO.PathOf("ua_generated_root");

            public static string Core => Root + "Core/";

            public static string Resources => Core + "Editor/Resources/";
            public static string Logos => Resources + "Logos/";
            public static string Icons => Resources + "Icons/";
        }
}
