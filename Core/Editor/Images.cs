using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    public static class Images
    {
        public static Texture2D ualive_logo;
        public static Texture2D flow_icon_16;
        public static Texture2D flow_icon_32;
        public static Texture2D return_icon_16;
        public static Texture2D return_icon_32;
        public static Texture2D variables_16;
        public static Texture2D variables_32;
        public static Texture2D override_16;
        public static Texture2D override_32;
        public static Texture2D property_16;
        public static Texture2D property_32;
        public static Texture2D special_16;
        public static Texture2D special_32;
        public static Texture2D class_16;
        public static Texture2D class_32;
        public static Texture2D class_variable_32;
        public static Texture2D parameters_16;
        public static Texture2D invoke_32;
        public static Texture2D compilation_16;
        public static Texture2D search_16;
        public static Texture2D explorer_16;
        public static Texture2D void_32;
        public static Texture2D this_32;
        public static Texture2D foldout_closed;
        public static Texture2D foldout_open;

        public static Texture2D return_event_32;
        public static Texture2D binary_save_32;
        public static Texture2D multi_array_32;
        public static Texture2D value_reroute_32;
        public static Texture2D flow_reroute_32;

        private static bool cached;

        public static void Cache()
        {
            if (!cached)
            {
                Logos("ualive", out ualive_logo);
                Icons("flow_16", out flow_icon_16);
                Icons("flow_32", out flow_icon_32);
                Icons("return_16", out return_icon_16);
                Icons("return_32", out return_icon_32);
                Icons("variables_16", out variables_16);
                Icons("variables_32", out variables_32);
                Icons("override_16", out override_16);
                Icons("override_32", out override_32);
                Icons("property_16", out property_16);
                Icons("property_32", out property_32);
                Icons("special_16", out special_16);
                Icons("special_32", out special_32);
                Icons("class_16", out class_16);
                Icons("class_32", out class_32);
                Icons("class_variable_32", out class_variable_32);
                Icons("parameters_16", out parameters_16);
                Icons("invoke_32", out invoke_32);
                Icons("compilation_16", out compilation_16);
                Icons("search_16", out search_16);
                Icons("explorer_16", out explorer_16);
                Icons("void_32", out void_32);
                Icons("this_32", out this_32);

                Icons("return_event_32", out return_event_32);
                Icons("binary_save_32", out binary_save_32);
                Icons("multi_array_32", out multi_array_32);
                Icons("value_reroute_32", out value_reroute_32);
                Icons("flow_reroute_32", out flow_reroute_32);

                cached = true;
            }
        }

        public static void Reset()
        {
            cached = false;
        }

        private static void Logos(string filename, out Texture2D texture)
        {
            var path = UAPaths.Logos + filename + ".png";
            texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        }

        private static void Icons(string filename, out Texture2D texture)
        {
            var path = UAPaths.Icons + filename + ".png";
            texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        }
    }
}
