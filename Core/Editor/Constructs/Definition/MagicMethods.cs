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
                var definer = methods.Definer() as MethodDefiner;
                var Awake = definer.SetMethod(@class, new MethodDeclaration("Awake", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnEnable = definer.SetMethod(@class, new MethodDeclaration("OnEnable", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnDisable = definer.SetMethod(@class, new MethodDeclaration("OnDisable", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var Start = definer.SetMethod(@class, new MethodDeclaration("Start", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var Update = definer.SetMethod(@class, new MethodDeclaration("Update", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnDestroy = definer.SetMethod(@class, new MethodDeclaration("OnDestroy", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var FixedUpdate = definer.SetMethod(@class, new MethodDeclaration("FixedUpdate", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var LateUpdate = definer.SetMethod(@class, new MethodDeclaration("LateUpdate", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnGUI = definer.SetMethod(@class, new MethodDeclaration("OnGUI", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnAnimatorIK = definer.SetMethod(@class, new MethodDeclaration("OnAnimatorIK", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("layerIndex", typeof(int)) }, true));
                var OnAnimatorMove = definer.SetMethod(@class, new MethodDeclaration("OnAnimatorMove", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnApplicationFocus = definer.SetMethod(@class, new MethodDeclaration("OnApplicationFocus", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("hasFocus", typeof(bool)) }, true));
                var OnApplicationPause = definer.SetMethod(@class, new MethodDeclaration("OnApplicationPause", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("pauseStatus", typeof(bool)) }, true));
                var OnApplicationQuit = definer.SetMethod(@class, new MethodDeclaration("OnApplicationQuit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnAudioFilterRead = definer.SetMethod(@class, new MethodDeclaration("OnAudioFilterRead", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("data", typeof(float[])), new ParameterDeclaration("channels", typeof(int)) }, true));
                var OnBecameInvisible = definer.SetMethod(@class, new MethodDeclaration("OnBecameInvisible", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collision", typeof(Collision)) }, true));
                var OnBecameVisible = definer.SetMethod(@class, new MethodDeclaration("OnBecameVisible", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("col", typeof(Collision)) }, true));
                var OnCollisionEnter = definer.SetMethod(@class, new MethodDeclaration("OnCollisionEnter", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collision", typeof(Collision)) }, true));
                var OnCollisionEnter2D = definer.SetMethod(@class, new MethodDeclaration("OnCollisionEnter2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("col", typeof(Collision2D)) }, true));
                var OnCollisionExit = definer.SetMethod(@class, new MethodDeclaration("OnCollisionExit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("other", typeof(Collision)) }, true));
                var OnCollisionExit2D = definer.SetMethod(@class, new MethodDeclaration("OnCollisionExit2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("other", typeof(Collision2D)) }, true));
                var OnCollisionStay = definer.SetMethod(@class, new MethodDeclaration("OnCollisionStay", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collisionInfo", typeof(Collision)) }, true));
                var OnCollisionStay2D = definer.SetMethod(@class, new MethodDeclaration("OnCollisionStay2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("other", typeof(Collision2D)) }, true));
                var OnConnectedToServer = definer.SetMethod(@class, new MethodDeclaration("OnConnectedToServer", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("hit", typeof(ControllerColliderHit)) }, true));
                var OnControllerColliderHit = definer.SetMethod(@class, new MethodDeclaration("OnControllerColliderHit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnDrawGizmos = definer.SetMethod(@class, new MethodDeclaration("OnDrawGizmos", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnDrawGizmosSelected = definer.SetMethod(@class, new MethodDeclaration("OnDrawGizmosSelected", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnJointBreak = definer.SetMethod(@class, new MethodDeclaration("OnJointBreak", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("breakForce", typeof(float)) }, true));
                var OnJointBreak2D = definer.SetMethod(@class, new MethodDeclaration("OnJointBreak2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("brokenJoint", typeof(Joint2D)) }, true));
                var OnMouseDown = definer.SetMethod(@class, new MethodDeclaration("OnMouseDown", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnMouseDrag = definer.SetMethod(@class, new MethodDeclaration("OnMouseDrag", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnMouseEnter = definer.SetMethod(@class, new MethodDeclaration("OnMouseEnter", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnMouseExit = definer.SetMethod(@class, new MethodDeclaration("OnMouseExit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnMouseOver = definer.SetMethod(@class, new MethodDeclaration("OnMouseOver", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnMouseUp = definer.SetMethod(@class, new MethodDeclaration("OnMouseUp", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnMouseUpAsButton = definer.SetMethod(@class, new MethodDeclaration("OnMouseUpAsButton", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnParticleCollision = definer.SetMethod(@class, new MethodDeclaration("OnParticleCollision", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("other", typeof(GameObject)) }, true));
                var OnParticleSystemStopped = definer.SetMethod(@class, new MethodDeclaration("OnParticleSystemStopped", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnParticleTrigger = definer.SetMethod(@class, new MethodDeclaration("OnParticleTrigger", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnParticleUpdateJobScheduled = definer.SetMethod(@class, new MethodDeclaration("OnParticleUpdateJobScheduled", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnPostRender = definer.SetMethod(@class, new MethodDeclaration("OnPostRender", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnPreCull = definer.SetMethod(@class, new MethodDeclaration("OnPreCull", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnPreRender = definer.SetMethod(@class, new MethodDeclaration("OnPreRender", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnRenderImage = definer.SetMethod(@class, new MethodDeclaration("OnRenderImage", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("src", typeof(RenderTexture)), new ParameterDeclaration("dest", typeof(RenderTexture)) }, true));
                var OnRenderObject = definer.SetMethod(@class, new MethodDeclaration("OnRenderObject", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnTransformChildrenChanged = definer.SetMethod(@class, new MethodDeclaration("OnTransformChildrenChanged", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnTransformParentChanged = definer.SetMethod(@class, new MethodDeclaration("OnTransformParentChanged", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnTriggerEnter = definer.SetMethod(@class, new MethodDeclaration("OnTriggerEnter", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider)) }, true));
                var OnTriggerEnter2D = definer.SetMethod(@class, new MethodDeclaration("OnTriggerEnter2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider2D)) }, true));
                var OnTriggerExit = definer.SetMethod(@class, new MethodDeclaration("OnTriggerExit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider)) }, true));
                var OnTriggerExit2D = definer.SetMethod(@class, new MethodDeclaration("OnTriggerExit2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider2D)) }, true));
                var OnTriggerStay = definer.SetMethod(@class, new MethodDeclaration("OnTriggerStay", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider)) }, true));
                var OnTriggerStay2D = definer.SetMethod(@class, new MethodDeclaration("OnTriggerStay2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider2D)) }, true));
                var OnValidate = definer.SetMethod(@class, new MethodDeclaration("OnValidate", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnWillRenderObject = definer.SetMethod(@class, new MethodDeclaration("OnWillRenderObject", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var Reset = definer.SetMethod(@class, new MethodDeclaration("Reset", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                return true;
            }
            return false;
        }

        public static bool TryAddScriptableObject(Methods methods, CustomClass @class, Inheritance inheritance)
        {
            if (inheritance.Inherits<ScriptableObject>() && !inheritance.Inherits<EditorWindow>())
            {
                var definer = methods.Definer() as MethodDefiner;
                var Awake = definer.SetMethod(@class, new MethodDeclaration("Awake", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnEnable = definer.SetMethod(@class, new MethodDeclaration("OnEnable", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnDisable = definer.SetMethod(@class, new MethodDeclaration("OnDisable", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnDestroy = definer.SetMethod(@class, new MethodDeclaration("OnDestroy", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                return true;
            }
            return false;
        }


        public static bool TryAddEditorWindow(Methods methods, CustomClass @class, Inheritance inheritance)
        {
            if (inheritance.Inherits<EditorWindow>())
            {
                var definer = methods.Definer() as MethodDefiner;
                var Awake = definer.SetMethod(@class, new MethodDeclaration("Awake", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var Update = definer.SetMethod(@class, new MethodDeclaration("Update", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnDestroy = definer.SetMethod(@class, new MethodDeclaration("OnDestroy", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnEnable = definer.SetMethod(@class, new MethodDeclaration("OnFocus", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnGUI = definer.SetMethod(@class, new MethodDeclaration("OnGUI", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnHierarchyChange = definer.SetMethod(@class, new MethodDeclaration("OnHierarchyChange", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnInspectorUpdate = definer.SetMethod(@class, new MethodDeclaration("OnInspectorUpdate", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnLostFocus = definer.SetMethod(@class, new MethodDeclaration("OnLostFocus", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnProjectChange = definer.SetMethod(@class, new MethodDeclaration("OnProjectChange", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                var OnSelectionChange = definer.SetMethod(@class, new MethodDeclaration("OnSelectionChange", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true));
                return true;
            }
            return false;
        }
    }
}
