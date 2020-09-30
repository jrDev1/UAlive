namespace Lasm.UAlive
{
    [Definer(typeof(Methods))]
    public sealed class MethodDefiner : Definer<Methods>
    {
        public override void Undefine()
        {
            decorated.overrides.Undefine(ref _defineRemoved, (method) =>
            {
                UnityEngine.Object.DestroyImmediate(method, true);
            });

            if (defineRemoved)
            {
                Changed();
            }
        }

        private void DefineMethod(string name, Method method)
        {
            method.name = name;
            method.entry.Define();
        }

        /// <summary>
        /// Attempts to create a new method if it does not exist. If it does, it ensures the parameters are the same.
        /// </summary>
        public Method SetMethod(CustomClass instance, MethodDeclaration declaration)
        {
            Method _method = null;

            // If this is an override method we will attempt to create and define the method.
            if (instance != null)
            {
                _method = decorated.overrides.Define(declaration.name, ref _defineAdded,
                (method) =>
                {
                    var newMethod = Method.Create(instance);
                    return newMethod;
                }, null);

                if (_method != null)
                {
                    _method.entry.declaration.Copy(declaration);
                    _method.name = declaration.name;
                    _method.entry.Define();
                    _method.entry.DefineReturns();
                }
            }

            return _method;
        }
    }
}