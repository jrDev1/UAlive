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
                var Awake = methods.New(@class, "Awake", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnEnable = methods.New(@class, "OnEnable", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnDisable = methods.New(@class, "OnDisable", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var Start = methods.New(@class, "Start", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var Update = methods.New(@class, "Update", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnDestroy = methods.New(@class, "OnDestroy", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var FixedUpdate = methods.New(@class, "FixedUpdate", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var LateUpdate = methods.New(@class, "LateUpdate", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnGUI = methods.New(@class, "OnGUI", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnAnimatorIK = methods.New(@class, "OnAnimatorIK", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("layerIndex", typeof(int)) }, true);
                var OnAnimatorMove = methods.New(@class, "OnAnimatorMove", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnApplicationFocus = methods.New(@class, "OnApplicationFocus", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("hasFocus", typeof(bool)) }, true);
                var OnApplicationPause = methods.New(@class, "OnApplicationPause", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("pauseStatus", typeof(bool)) }, true);
                var OnApplicationQuit = methods.New(@class, "OnApplicationQuit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnAudioFilterRead = methods.New(@class, "OnAudioFilterRead", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("data", typeof(float[])), new ParameterDeclaration("channels", typeof(int)) }, true);
                var OnBecameInvisible = methods.New(@class, "OnBecameInvisible", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collision", typeof(Collision)) }, true);
                var OnBecameVisible = methods.New(@class, "OnBecameVisible", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("col", typeof(Collision)) }, true);
                var OnCollisionEnter = methods.New(@class, "OnCollisionEnter", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collision", typeof(Collision)) }, true);
                var OnCollisionEnter2D = methods.New(@class, "OnCollisionEnter2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("col", typeof(Collision2D)) }, true);
                var OnCollisionExit = methods.New(@class, "OnCollisionExit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("other", typeof(Collision)) }, true);
                var OnCollisionExit2D = methods.New(@class, "OnCollisionExit2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("other", typeof(Collision2D)) }, true);
                var OnCollisionStay = methods.New(@class, "OnCollisionStay", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collisionInfo", typeof(Collision)) }, true);
                var OnCollisionStay2D = methods.New(@class, "OnCollisionStay2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("other", typeof(Collision2D)) }, true);
                var OnConnectedToServer = methods.New(@class, "OnConnectedToServer", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("hit", typeof(ControllerColliderHit)) }, true);
                var OnControllerColliderHit = methods.New(@class, "OnControllerColliderHit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnDrawGizmos = methods.New(@class, "OnDrawGizmos", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnDrawGizmosSelected = methods.New(@class, "OnDrawGizmosSelected", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnJointBreak = methods.New(@class, "OnJointBreak", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("breakForce", typeof(float)) }, true);
                var OnJointBreak2D = methods.New(@class, "OnJointBreak2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("brokenJoint", typeof(Joint2D)) }, true);
                var OnMouseDown = methods.New(@class, "OnMouseDown", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnMouseDrag = methods.New(@class, "OnMouseDrag", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnMouseEnter = methods.New(@class, "OnMouseEnter", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnMouseExit = methods.New(@class, "OnMouseExit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnMouseOver = methods.New(@class, "OnMouseOver", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnMouseUp = methods.New(@class, "OnMouseUp", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnMouseUpAsButton = methods.New(@class, "OnMouseUpAsButton", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnParticleCollision = methods.New(@class, "OnParticleCollision", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("other", typeof(GameObject)) }, true);
                var OnParticleSystemStopped = methods.New(@class, "OnParticleSystemStopped", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnParticleTrigger = methods.New(@class, "OnParticleTrigger", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnParticleUpdateJobScheduled = methods.New(@class, "OnParticleUpdateJobScheduled", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnPostRender = methods.New(@class, "OnPostRender", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnPreCull = methods.New(@class, "OnPreCull", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnPreRender = methods.New(@class, "OnPreRender", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnRenderImage = methods.New(@class, "OnRenderImage", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("src", typeof(RenderTexture)), new ParameterDeclaration("dest", typeof(RenderTexture)) }, true);
                var OnRenderObject = methods.New(@class, "OnRenderObject", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnTransformChildrenChanged = methods.New(@class, "OnTransformChildrenChanged", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnTransformParentChanged = methods.New(@class, "OnTransformParentChanged", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnTriggerEnter = methods.New(@class, "OnTriggerEnter", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider)) }, true);
                var OnTriggerEnter2D = methods.New(@class, "OnTriggerEnter2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider2D)) }, true);
                var OnTriggerExit = methods.New(@class, "OnTriggerExit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider)) }, true);
                var OnTriggerExit2D = methods.New(@class, "OnTriggerExit2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider2D)) }, true);
                var OnTriggerStay = methods.New(@class, "OnTriggerStay", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider)) }, true);
                var OnTriggerStay2D = methods.New(@class, "OnTriggerStay2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider2D)) }, true);
                var OnValidate = methods.New(@class, "OnValidate", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnWillRenderObject = methods.New(@class, "OnWillRenderObject", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var Reset = methods.New(@class, "Reset", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                return true;
            }
            return false;
        }

        public static bool TryAddScriptableObject(Methods methods, CustomClass @class, Inheritance inheritance)
        {
            if (inheritance.Inherits<ScriptableObject>())
            {
                var Awake = methods.New(@class, "Awake", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnEnable = methods.New(@class, "OnEnable", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnDisable = methods.New(@class, "OnDisable", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnDestroy = methods.New(@class, "OnDestroy", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                return true;
            }
            return false;
        }


        public static bool TryAddEditorWindow(Methods methods, CustomClass @class, Inheritance inheritance)
        {
            if (inheritance.Inherits<EditorWindow>())
            {
                var Awake = methods.New(@class, "Awake", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var Update = methods.New(@class, "Update", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnDestroy = methods.New(@class, "OnDestroy", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnEnable = methods.New(@class, "OnFocus", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnGUI = methods.New(@class, "OnGUI", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnHierarchyChange = methods.New(@class, "OnHierarchyChange", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnInspectorUpdate = methods.New(@class, "OnInspectorUpdate", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnLostFocus = methods.New(@class, "OnLostFocus", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnProjectChange = methods.New(@class, "OnProjectChange", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnSelectionChange = methods.New(@class, "OnSelectionChange", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                return true;
            }
            return false;
        }
    }
}
