using Ludiq;
using System;

namespace Lasm.UAlive
{
    [Serializable]
    [IncludeInSettings(true)]
    public sealed class UAClass
    {
        [Serialize]
        private ClassVariables variables = new ClassVariables();
        [Serialize]
        internal ClassMacro macro;
        private string GUID;

        public UAClass(string GUID)
        {
            this.GUID = GUID;
        }

        private void EnsureInitialized(string GUID)
        {
            ClassExtensions.GetClass(ref macro, GUID);
        }

        public void Invoke(IUAClass @class, string name, Action<object> returnMethod, params object[] parameters)
        {
            EnsureInitialized(GUID);
            ClassExtensions.Invoke(@class, name, returnMethod, parameters);
        }

        public T Get<T>(string name)
        {
            EnsureInitialized(GUID);
            return variables.Get<T>(macro, name);
        }

        public object Get(string name)
        {
            EnsureInitialized(GUID);
            return variables.Get(macro, name);
        }

        public void Set(string name, object value)
        {
            EnsureInitialized(GUID);
            variables.Set(macro, name, value);
        }
    }
}