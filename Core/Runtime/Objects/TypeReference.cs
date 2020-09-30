using Lasm.OdinSerializer;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    public sealed class TypeReference : SerializedScriptableObject
    {
        [OdinSerialize]
        private Dictionary<string, CustomType> types = new Dictionary<string, CustomType>();

#if UNITY_EDITOR
        public void Add(CustomType type)
        {
            var guid = type.GetGUID();
            if (!types.ContainsKey(guid)) types.Add(guid, type);
        }
#endif

        public CustomType Get(string guid)
        {
            return types[guid];
        }
    }
}