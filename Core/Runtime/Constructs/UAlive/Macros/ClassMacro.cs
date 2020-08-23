using Bolt;
using Ludiq;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    public sealed class ClassMacro : TypeMacro
    {
        [Serialize]
        public Inheritance inheritance = new Inheritance();

        [Serialize]
        public MethodCollection methods = new MethodCollection();

        [Serialize]
        public Variables variables = new Variables();

#if UNITY_EDITOR
        public bool customOpen;
        public bool overridesOpen;
        public bool customVariablesOpen;
        public bool customMethodsOpen;
        public bool methodOverridesOpen;
        public bool propertyOverridesOpen;
#endif

        [MenuItem("Assets/Create/UAlive/Custom Class", priority = 0)]
        private static void CreateClassMacro()
        {
            var macro = CreateAsset<ClassMacro>("Custom Class");
            if (macro != null)
            {
                Selection.activeObject = macro;
            }
        }

        protected override void BeforeDefine()
        {
            methods.overrides.Clear();
            base.BeforeDefine();
        }

        protected override void AfterDefine()
        {
            base.AfterDefine();
            methods.RemoveUnusedMethods();
        }

        protected override void Definition()
        {
            foreach (MethodInfo method in inheritance.type.GetMethods())
            {
                if (method.Overridable())
                {
                    List<ParameterDeclaration> methodParams = new List<ParameterDeclaration>();

                    foreach (ParameterInfo parameter in method.GetParameters())
                    { 
                        methodParams.Add(new ParameterDeclaration(parameter.Name, parameter.ParameterType));
                    }

                    var modifier = method.GetModifier() == MethodModifier.Abstract || method.GetModifier() == MethodModifier.Virtual ? MethodModifier.Override : method.GetModifier();
                    var meth = methods.New(this, method.Name, method.GetScope(), modifier, method.ReturnType, methodParams.ToArray());
                    if (method.IsVirtual) meth.hasOptionalOverride = true;
                }
            }

            MagicMethods();
        }

        private void MagicMethods()
        {
            if (MonoBehaviours()) return;
            if (ScriptableObjects()) return;
            if (EditorWindows()) return;
        }

        private bool MonoBehaviours()
        {
            if (inheritance.Inherits<MonoBehaviour>())
            {
                var Awake = methods.New(this, "Awake", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnEnable = methods.New(this, "OnEnable", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnDisable = methods.New(this, "OnDisable", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var Start = methods.New(this, "Start", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var Update = methods.New(this, "Update", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnDestroy = methods.New(this, "OnDestroy", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var FixedUpdate = methods.New(this, "FixedUpdate", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var LateUpdate = methods.New(this, "LateUpdate", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnGUI = methods.New(this, "OnGUI", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnAnimatorIK = methods.New(this, "OnAnimatorIK", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("layerIndex", typeof(int)) }, true);
                var OnAnimatorMove = methods.New(this, "OnAnimatorMove", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnApplicationFocus = methods.New(this, "OnApplicationFocus", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("hasFocus", typeof(bool)) }, true);
                var OnApplicationPause = methods.New(this, "OnApplicationPause", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("pauseStatus", typeof(bool)) }, true);
                var OnApplicationQuit = methods.New(this, "OnApplicationQuit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnAudioFilterRead = methods.New(this, "OnAudioFilterRead", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("data", typeof(float[])), new ParameterDeclaration("channels", typeof(int)) }, true);
                var OnBecameInvisible = methods.New(this, "OnBecameInvisible", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collision", typeof(Collision)) }, true);
                var OnBecameVisible = methods.New(this, "OnBecameVisible", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("col", typeof(Collision)) }, true);
                var OnCollisionEnter = methods.New(this, "OnCollisionEnter", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collision", typeof(Collision)) }, true);
                var OnCollisionEnter2D = methods.New(this, "OnCollisionEnter2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("col", typeof(Collision2D)) }, true);
                var OnCollisionExit = methods.New(this, "OnCollisionExit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("other", typeof(Collision)) }, true);
                var OnCollisionExit2D = methods.New(this, "OnCollisionExit2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("other", typeof(Collision2D)) }, true);
                var OnCollisionStay = methods.New(this, "OnCollisionStay", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collisionInfo", typeof(Collision)) }, true);
                var OnCollisionStay2D = methods.New(this, "OnCollisionStay2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("other", typeof(Collision2D)) }, true);
                var OnConnectedToServer = methods.New(this, "OnConnectedToServer", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("hit", typeof(ControllerColliderHit)) }, true);
                var OnControllerColliderHit = methods.New(this, "OnControllerColliderHit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnDrawGizmos = methods.New(this, "OnDrawGizmos", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnDrawGizmosSelected = methods.New(this, "OnDrawGizmosSelected", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnJointBreak = methods.New(this, "OnJointBreak", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("breakForce", typeof(float)) }, true);
                var OnJointBreak2D = methods.New(this, "OnJointBreak2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("brokenJoint", typeof(Joint2D)) }, true);
                var OnMouseDown = methods.New(this, "OnMouseDown", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnMouseDrag = methods.New(this, "OnMouseDrag", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnMouseEnter = methods.New(this, "OnMouseEnter", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnMouseExit = methods.New(this, "OnMouseExit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnMouseOver = methods.New(this, "OnMouseOver", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnMouseUp = methods.New(this, "OnMouseUp", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnMouseUpAsButton = methods.New(this, "OnMouseUpAsButton", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnParticleCollision = methods.New(this, "OnParticleCollision", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("other", typeof(GameObject)) }, true);
                var OnParticleSystemStopped = methods.New(this, "OnParticleSystemStopped", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnParticleTrigger = methods.New(this, "OnParticleTrigger", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnParticleUpdateJobScheduled = methods.New(this, "OnParticleUpdateJobScheduled", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnPostRender = methods.New(this, "OnPostRender", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnPreCull = methods.New(this, "OnPreCull", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnPreRender = methods.New(this, "OnPreRender", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnRenderImage = methods.New(this, "OnRenderImage", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("src", typeof(RenderTexture)), new ParameterDeclaration("dest", typeof(RenderTexture)) }, true);
                var OnRenderObject = methods.New(this, "OnRenderObject", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnTransformChildrenChanged = methods.New(this, "OnTransformChildrenChanged", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnTransformParentChanged = methods.New(this, "OnTransformParentChanged", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnTriggerEnter = methods.New(this, "OnTriggerEnter", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider)) }, true);
                var OnTriggerEnter2D = methods.New(this, "OnTriggerEnter2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider2D)) }, true);
                var OnTriggerExit = methods.New(this, "OnTriggerExit", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider)) }, true);
                var OnTriggerExit2D = methods.New(this, "OnTriggerExit2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider2D)) }, true);
                var OnTriggerStay = methods.New(this, "OnTriggerStay", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider)) }, true);
                var OnTriggerStay2D = methods.New(this, "OnTriggerStay2D", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { new ParameterDeclaration("collider", typeof(Collider2D)) }, true);
                var OnValidate = methods.New(this, "OnValidate", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnWillRenderObject = methods.New(this, "OnWillRenderObject", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var Reset = methods.New(this, "Reset", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);

                return true;
            }

            return false;
        }

        private bool ScriptableObjects()
        {
            if (inheritance.Inherits<ScriptableObject>())
            {
                var Awake = methods.New(this, "Awake", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnEnable = methods.New(this, "OnEnable", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnDisable = methods.New(this, "OnDisable", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnDestroy = methods.New(this, "OnDestroy", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                return true;
            }

            return false;
        }

#if UNITY_EDITOR
        private bool EditorWindows()
        {
            if (inheritance.Inherits<EditorWindow>())
            {
                var Awake = methods.New(this, "Awake", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var Update = methods.New(this, "Update", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnDestroy = methods.New(this, "OnDestroy", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnEnable = methods.New(this, "OnFocus", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnGUI = methods.New(this, "OnGUI", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnHierarchyChange = methods.New(this, "OnHierarchyChange", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnInspectorUpdate = methods.New(this, "OnInspectorUpdate", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnLostFocus = methods.New(this, "OnLostFocus", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnProjectChange = methods.New(this, "OnProjectChange", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                var OnSelectionChange = methods.New(this, "OnSelectionChange", AccessModifier.Private, MethodModifier.None, typeof(void), new ParameterDeclaration[] { }, true);
                return true;
            }

            return false;
        }
        
#endif

        protected override void RefreshOnChange()
        {
            base.RefreshOnChange();

            if (methods.CanAdd())
            {
                methods.Refresh();
            }
        }
    }
}