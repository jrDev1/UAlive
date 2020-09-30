namespace Lasm.UAlive
{
    public static partial class UAPaths
    {
        private static string _root;
        public static string Root
        {
            get
            {
                if (string.IsNullOrEmpty(_root)) _root = HUMIO.PathOf("ua_root");
                return _root;
            }
        }

        public static string Core => Root + "Core/";
        public static string Generated => Root + "Generated/";

        public static string Resources => Core + "Editor/Resources/";
        public static string Logos => Resources + "Logos/";
        public static string Icons => Resources + "Icons/";
    }
}
