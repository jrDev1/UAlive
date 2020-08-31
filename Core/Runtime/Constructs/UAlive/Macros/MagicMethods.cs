using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    public static class MagicMethods
    {
        public static bool TryAddMonoBehaviour(Methods methods, CustomClass @class, Inheritance inheritance)
        {

            if (inheritance.Inherits<MonoBehaviour>())
            {
                var Awake = methods.TryCreateMethod(@class, new MethodDeclaration("Awake", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnEnable = methods.TryCreateMethod(@class, new MethodDeclaration("OnEnable", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnDisable = methods.TryCreateMethod(@class, new MethodDeclaration("OnDisable", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var Start = methods.TryCreateMethod(@class, new MethodDeclaration("Start", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var Update = methods.TryCreateMethod(@class, new MethodDeclaration("Update", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnDestroy = methods.TryCreateMethod(@class, new MethodDeclaration("OnDestroy", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var FixedUpdate = methods.TryCreateMethod(@class, new MethodDeclaration("FixedUpdate", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var LateUpdate = methods.TryCreateMethod(@class, new MethodDeclaration("LateUpdate", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnGUI = methods.TryCreateMethod(@class, new MethodDeclaration("OnGUI", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnAnimatorIK = methods.TryCreateMethod(@class, new MethodDeclaration("OnAnimatorIK", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("layerIndex", typeof(int)) }, true));
                var OnAnimatorMove = methods.TryCreateMethod(@class, new MethodDeclaration("OnAnimatorMove", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnApplicationFocus = methods.TryCreateMethod(@class, new MethodDeclaration("OnApplicationFocus", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("hasFocus", typeof(bool)) }, true));
                var OnApplicationPause = methods.TryCreateMethod(@class, new MethodDeclaration("OnApplicationPause", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("pauseStatus", typeof(bool)) }, true));
                var OnApplicationQuit = methods.TryCreateMethod(@class, new MethodDeclaration("OnApplicationQuit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnAudioFilterRead = methods.TryCreateMethod(@class, new MethodDeclaration("OnAudioFilterRead", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("data", typeof(float[])), new ParameterDeclaration("channels", typeof(int)) }, true));
                var OnBecameInvisible = methods.TryCreateMethod(@class, new MethodDeclaration("OnBecameInvisible", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collision", typeof(Collision)) }, true));
                var OnBecameVisible = methods.TryCreateMethod(@class, new MethodDeclaration("OnBecameVisible", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("col", typeof(Collision)) }, true));
                var OnCollisionEnter = methods.TryCreateMethod(@class, new MethodDeclaration("OnCollisionEnter", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collision", typeof(Collision)) }, true));
                var OnCollisionEnter2D = methods.TryCreateMethod(@class, new MethodDeclaration("OnCollisionEnter2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("col", typeof(Collision2D)) }, true));
                var OnCollisionExit = methods.TryCreateMethod(@class, new MethodDeclaration("OnCollisionExit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("other", typeof(Collision)) }, true));
                var OnCollisionExit2D = methods.TryCreateMethod(@class, new MethodDeclaration("OnCollisionExit2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("other", typeof(Collision2D)) }, true));
                var OnCollisionStay = methods.TryCreateMethod(@class, new MethodDeclaration("OnCollisionStay", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collisionInfo", typeof(Collision)) }, true));
                var OnCollisionStay2D = methods.TryCreateMethod(@class, new MethodDeclaration("OnCollisionStay2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("other", typeof(Collision2D)) }, true));
                var OnConnectedToServer = methods.TryCreateMethod(@class, new MethodDeclaration("OnConnectedToServer", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("hit", typeof(ControllerColliderHit)) }, true));
                var OnControllerColliderHit = methods.TryCreateMethod(@class, new MethodDeclaration("OnControllerColliderHit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnDrawGizmos = methods.TryCreateMethod(@class, new MethodDeclaration("OnDrawGizmos", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnDrawGizmosSelected = methods.TryCreateMethod(@class, new MethodDeclaration("OnDrawGizmosSelected", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnJointBreak = methods.TryCreateMethod(@class, new MethodDeclaration("OnJointBreak", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("breakForce", typeof(float)) }, true));
                var OnJointBreak2D = methods.TryCreateMethod(@class, new MethodDeclaration("OnJointBreak2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("brokenJoint", typeof(Joint2D)) }, true));
                var OnMouseDown = methods.TryCreateMethod(@class, new MethodDeclaration("OnMouseDown", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnMouseDrag = methods.TryCreateMethod(@class, new MethodDeclaration("OnMouseDrag", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnMouseEnter = methods.TryCreateMethod(@class, new MethodDeclaration("OnMouseEnter", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnMouseExit = methods.TryCreateMethod(@class, new MethodDeclaration("OnMouseExit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnMouseOver = methods.TryCreateMethod(@class, new MethodDeclaration("OnMouseOver", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnMouseUp = methods.TryCreateMethod(@class, new MethodDeclaration("OnMouseUp", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnMouseUpAsButton = methods.TryCreateMethod(@class, new MethodDeclaration("OnMouseUpAsButton", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnParticleCollision = methods.TryCreateMethod(@class, new MethodDeclaration("OnParticleCollision", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("other", typeof(GameObject)) }, true));
                var OnParticleSystemStopped = methods.TryCreateMethod(@class, new MethodDeclaration("OnParticleSystemStopped", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnParticleTrigger = methods.TryCreateMethod(@class, new MethodDeclaration("OnParticleTrigger", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnParticleUpdateJobScheduled = methods.TryCreateMethod(@class, new MethodDeclaration("OnParticleUpdateJobScheduled", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnPostRender = methods.TryCreateMethod(@class, new MethodDeclaration("OnPostRender", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnPreCull = methods.TryCreateMethod(@class, new MethodDeclaration("OnPreCull", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnPreRender = methods.TryCreateMethod(@class, new MethodDeclaration("OnPreRender", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnRenderImage = methods.TryCreateMethod(@class, new MethodDeclaration("OnRenderImage", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("src", typeof(RenderTexture)), new ParameterDeclaration("dest", typeof(RenderTexture)) }, true));
                var OnRenderObject = methods.TryCreateMethod(@class, new MethodDeclaration("OnRenderObject", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnTransformChildrenChanged = methods.TryCreateMethod(@class, new MethodDeclaration("OnTransformChildrenChanged", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnTransformParentChanged = methods.TryCreateMethod(@class, new MethodDeclaration("OnTransformParentChanged", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnTriggerEnter = methods.TryCreateMethod(@class, new MethodDeclaration("OnTriggerEnter", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider)) }, true));
                var OnTriggerEnter2D = methods.TryCreateMethod(@class, new MethodDeclaration("OnTriggerEnter2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider2D)) }, true));
                var OnTriggerExit = methods.TryCreateMethod(@class, new MethodDeclaration("OnTriggerExit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider)) }, true));
                var OnTriggerExit2D = methods.TryCreateMethod(@class, new MethodDeclaration("OnTriggerExit2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider2D)) }, true));
                var OnTriggerStay = methods.TryCreateMethod(@class, new MethodDeclaration("OnTriggerStay", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider)) }, true));
                var OnTriggerStay2D = methods.TryCreateMethod(@class, new MethodDeclaration("OnTriggerStay2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider2D)) }, true));
                var OnValidate = methods.TryCreateMethod(@class, new MethodDeclaration("OnValidate", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnWillRenderObject = methods.TryCreateMethod(@class, new MethodDeclaration("OnWillRenderObject", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var Reset = methods.TryCreateMethod(@class, new MethodDeclaration("Reset", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                return true;
            }
            return false;
        }

        public static bool TryAddScriptableObject(Methods methods, CustomClass @class, Inheritance inheritance)
        {
            if (inheritance.Inherits<ScriptableObject>())
            {
                var Awake = methods.TryCreateMethod(@class, new MethodDeclaration("Awake", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnEnable = methods.TryCreateMethod(@class, new MethodDeclaration("OnEnable", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnDisable = methods.TryCreateMethod(@class, new MethodDeclaration("OnDisable", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnDestroy = methods.TryCreateMethod(@class, new MethodDeclaration("OnDestroy", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                return true;
            }
            return false;
        }


        public static bool TryAddEditorWindow(Methods methods, CustomClass @class, Inheritance inheritance)
        {
            if (inheritance.Inherits<EditorWindow>())
            {
                var Awake = methods.TryCreateMethod(@class, new MethodDeclaration("Awake", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var Update = methods.TryCreateMethod(@class, new MethodDeclaration("Update", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnDestroy = methods.TryCreateMethod(@class, new MethodDeclaration("OnDestroy", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnEnable = methods.TryCreateMethod(@class, new MethodDeclaration("OnFocus", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnGUI = methods.TryCreateMethod(@class, new MethodDeclaration("OnGUI", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnHierarchyChange = methods.TryCreateMethod(@class, new MethodDeclaration("OnHierarchyChange", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnInspectorUpdate = methods.TryCreateMethod(@class, new MethodDeclaration("OnInspectorUpdate", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnLostFocus = methods.TryCreateMethod(@class, new MethodDeclaration("OnLostFocus", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnProjectChange = methods.TryCreateMethod(@class, new MethodDeclaration("OnProjectChange", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnSelectionChange = methods.TryCreateMethod(@class, new MethodDeclaration("OnSelectionChange", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                return true;
            }
            return false;
        }
    }
}
