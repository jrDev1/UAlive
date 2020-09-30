using Lasm.OdinSerializer;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    public sealed class TypeReference : SerializedScriptableObject
    {
        [OdinSerialize]
        private Dictionary<int, CustomType> types = new Dictionary<int, CustomType>();

#if UNITY_EDITOR
        public void Add(CustomType type)
        {
            types.Add(type.GetGUID().ToInt(), type);
        }
#endif

        public CustomType Get(int guid)
        {
            return types[guid];
        }
    }
}