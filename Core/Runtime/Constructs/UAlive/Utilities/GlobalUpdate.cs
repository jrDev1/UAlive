using UnityEditor;

namespace Lasm.UAlive
{
    public sealed class GlobalUpdate
    {
        private bool isInitialized;

        public void Bind()
        {
            if (!isInitialized)
            {
                EditorApplication.update += UpdateProcess;
                isInitialized = true;
            }
        }

        public void Unbind()
        {
            if (isInitialized)
            {
                EditorApplication.update -= UpdateProcess;
                isInitialized = false;
            }
        }

        private void UpdateProcess()
        {

        }
    }
}