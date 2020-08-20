using Bolt;
using Ludiq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Lasm.UAlive
{
    public sealed class ClassMacro : ObjectMacro
    {
        [Serialize]
        private Type _inherits = typeof(object);
        [Inspectable]
        [Ludiq.TypeFilter(Structs = false, Sealed = false, OpenConstructedGeneric = false, NonPublic = false, Obsolete = false, Enums = false, Classes = true, Abstract = true, Static = false, Interfaces = false)]
        public Type inherits
        {
            get => _inherits;
            set
            {
                _inherits = value;
                Define();
            }
        }

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
            overrideMethods.Clear();
            base.BeforeDefine();
        }

        protected override void AfterDefine()
        {
            base.AfterDefine();
            RemoveUnusedMethods();
        }

        protected override void Definition()
        {
            base.Definition();

            foreach (MethodInfo method in inherits.GetMethods())
            {
                if (method.Overridable())
                {
                    List<(string name, Type type)> methodParams = new List<(string name, Type type)>();

                    foreach (ParameterInfo parameter in method.GetParameters())
                    { 
                        methodParams.Add((parameter.Name, parameter.ParameterType));
                    }

                    var modifier = method.GetModifier() == MethodModifier.Abstract || method.GetModifier() == MethodModifier.Virtual ? MethodModifier.Override : method.GetModifier();
                    var meth = MethodOverride(method.Name, method.GetScope(), modifier, method.ReturnType, method.Name, methodParams.ToArray());
                    if (method.IsVirtual) meth.hasOptionalOverride = true;
                }
            }

            foreach (Method method in methods)
            {
                CustomMethod(method.nest, method.name, title, AccessModifier.Public, MethodModifier.None, method.nest.returnType, method.name, null);
            }

            MagicMethods();

            for (int i = 0; i < variables.variables.Count; i++)
            {
                variables.variables[i].DefineGet();
                variables.variables[i].DefineSet();
            }

            for (int i = 0; i < methods.Count; i++)
            {
                methods[i].nest.macro.entry.Define(); 
            }
        }

        private void MagicMethods()
        {
            if (MonoBehaviours()) return;
            if (ScriptableObjects()) return;
            if (EditorWindows()) return;
        }

        public static void DefineAllGraphs()
        {
            var assets = HUMAssets.Find().Assets().OfType<ClassMacro>();
            Debug.Log(assets.Count);
            for (int i = 0; i < assets.Count; i++)
            {
                for (int j = 0; j < assets[i].methods.Count; j++)
                {
                    var units = assets[i].methods[j].nest.macro.graph.units.ToArrayPooled();
                    for (int unit = 0; unit < units.Length; unit++)
                    {
                        units[unit].Define();
                    }
                }
            }
        }

        public static void DefineAllGraphsOfType<T>() where T : IUnit
        {
            var assets = HUMAssets.Find().Assets().OfType<ClassMacro>();
            for (int i = 0; i < assets.Count; i++)
            {
                for (int j = 0; j < assets[i].methods.Count; j++)
                {
                    var units = assets[i].methods[j].nest.macro.graph.units.Where((u)=> { return u.GetType() == typeof(T); }).ToArrayPooled();
                    for (int unit = 0; unit < units.Length; unit++)
                    {
                        units[unit].Define();
                    }
                }
            }
        }

        private bool MonoBehaviours()
        {
            if (inherits.Inherits<MonoBehaviour>())
            {
                var Awake = MethodOverride("Awake", AccessModifier.Private, MethodModifier.None, typeof(void), "Awake", new(string name, Type type)[] { }, true);
                if (Awake != null) Awake.hasOptionalOverride = true;

                var OnEnable = MethodOverride("OnEnable", AccessModifier.Private, MethodModifier.None, typeof(void), "OnEnable", new(string name, Type type)[] { }, true);
                if (OnEnable != null) OnEnable.hasOptionalOverride = true;

                var OnDisable = MethodOverride("OnDisable", AccessModifier.Private, MethodModifier.None, typeof(void), "OnDisable", new(string name, Type type)[] { }, true);
                if (OnDisable != null) OnDisable.hasOptionalOverride = true;

                var Start = MethodOverride("Start", AccessModifier.Private, MethodModifier.None, typeof(void), "Start", new(string name, Type type)[] { }, true);
                if (Start != null) Start.hasOptionalOverride = true;

                var Update = MethodOverride("Update", AccessModifier.Private, MethodModifier.None, typeof(void), "Update", new(string name, Type type)[] { }, true);
                if (Update != null) Update.hasOptionalOverride = true;

                var OnDestroy = MethodOverride("OnDestroy", AccessModifier.Private, MethodModifier.None, typeof(void), "OnDestroy", new(string name, Type type)[] { }, true);
                if (OnDestroy != null) OnDestroy.hasOptionalOverride = true;

                var FixedUpdate = MethodOverride("FixedUpdate", AccessModifier.Private, MethodModifier.None, typeof(void), "FixedUpdate", new(string name, Type type)[] { }, true);
                if (FixedUpdate != null) FixedUpdate.hasOptionalOverride = true;

                var LateUpdate = MethodOverride("LateUpdate", AccessModifier.Private, MethodModifier.None, typeof(void), "LateUpdate", new(string name, Type type)[] { }, true);
                if (LateUpdate != null) LateUpdate.hasOptionalOverride = true;

                var OnGUI = MethodOverride("OnGUI", AccessModifier.Private, MethodModifier.None, typeof(void), "OnGUI", new(string name, Type type)[] { }, true);
                if (OnGUI != null) OnGUI.hasOptionalOverride = true;

                var OnAnimatorIK = MethodOverride("OnAnimatorIK", AccessModifier.Private, MethodModifier.None, typeof(void), "OnAnimatorIK", new(string name, Type type)[] { ("layerIndex", typeof(int)) }, true);
                if (OnAnimatorIK != null) OnAnimatorIK.hasOptionalOverride = true;

                var OnAnimatorMove = MethodOverride("OnAnimatorMove", AccessModifier.Private, MethodModifier.None, typeof(void), "OnAnimatorMove", new(string name, Type type)[] { }, true);
                if (OnAnimatorMove != null) OnAnimatorMove.hasOptionalOverride = true;

                var OnApplicationFocus = MethodOverride("OnApplicationFocus", AccessModifier.Private, MethodModifier.None, typeof(void), "OnApplicationFocus", new(string name, Type type)[] { ("hasFocus", typeof(bool)) }, true);
                if (OnApplicationFocus != null) OnApplicationFocus.hasOptionalOverride = true;

                var OnApplicationPause = MethodOverride("OnApplicationPause", AccessModifier.Private, MethodModifier.None, typeof(void), "OnApplicationPause", new(string name, Type type)[] { ("pauseStatus", typeof(bool)) }, true);
                if (OnApplicationPause != null) OnApplicationPause.hasOptionalOverride = true;

                var OnApplicationQuit = MethodOverride("OnApplicationQuit", AccessModifier.Private, MethodModifier.None, typeof(void), "OnApplicationQuit", new(string name, Type type)[] { }, true);
                if (OnApplicationQuit != null) OnApplicationQuit.hasOptionalOverride = true;

                var OnAudioFilterRead = MethodOverride("OnAudioFilterRead", AccessModifier.Private, MethodModifier.None, typeof(void), "OnAudioFilterRead", new(string name, Type type)[] { ("data", typeof(float[])), ("channels", typeof(int)) }, true);
                if (OnAudioFilterRead != null) OnAudioFilterRead.hasOptionalOverride = true;

                var OnBecameInvisible = MethodOverride("OnBecameInvisible", AccessModifier.Private, MethodModifier.None, typeof(void), "OnBecameInvisible", new(string name, Type type)[] { ("collision", typeof(Collision)) }, true);
                if (OnBecameInvisible != null) OnBecameInvisible.hasOptionalOverride = true;

                var OnBecameVisible = MethodOverride("OnBecameVisible", AccessModifier.Private, MethodModifier.None, typeof(void), "OnBecameVisible", new(string name, Type type)[] { ("col", typeof(Collision)) }, true);
                if (OnBecameVisible != null) OnBecameVisible.hasOptionalOverride = true;

                var OnCollisionEnter = MethodOverride("OnCollisionEnter", AccessModifier.Private, MethodModifier.None, typeof(void), "OnCollisionEnter", new(string name, Type type)[] { ("collision", typeof(Collision)) }, true);
                if (OnCollisionEnter != null) OnCollisionEnter.hasOptionalOverride = true;

                var OnCollisionEnter2D = MethodOverride("OnCollisionEnter2D", AccessModifier.Private, MethodModifier.None, typeof(void), "OnCollisionEnter2D", new(string name, Type type)[] { ("col", typeof(Collision2D)) }, true);
                if (OnCollisionEnter2D != null) OnCollisionEnter2D.hasOptionalOverride = true;

                var OnCollisionExit = MethodOverride("OnCollisionExit", AccessModifier.Private, MethodModifier.None, typeof(void), "OnCollisionExit", new(string name, Type type)[] { ("other", typeof(Collision)) }, true);
                if (OnCollisionExit != null) OnCollisionExit.hasOptionalOverride = true;

                var OnCollisionExit2D = MethodOverride("OnCollisionExit2D", AccessModifier.Private, MethodModifier.None, typeof(void), "OnCollisionExit2D", new(string name, Type type)[] { ("other", typeof(Collision2D)) }, true);
                if (OnCollisionExit2D != null) OnCollisionExit2D.hasOptionalOverride = true;

                var OnCollisionStay = MethodOverride("OnCollisionStay", AccessModifier.Private, MethodModifier.None, typeof(void), "OnCollisionStay", new(string name, Type type)[] { ("collisionInfo", typeof(Collision)) }, true);
                if (OnCollisionStay != null) OnCollisionStay.hasOptionalOverride = true;

                var OnCollisionStay2D = MethodOverride("OnCollisionStay2D", AccessModifier.Private, MethodModifier.None, typeof(void), "OnCollisionStay2D", new(string name, Type type)[] { ("other", typeof(Collision2D)) }, true);
                if (OnCollisionStay2D != null) OnCollisionStay2D.hasOptionalOverride = true;

                var OnConnectedToServer = MethodOverride("OnConnectedToServer", AccessModifier.Private, MethodModifier.None, typeof(void), "OnConnectedToServer", new(string name, Type type)[] { ("hit", typeof(ControllerColliderHit)) }, true);
                if (OnConnectedToServer != null) OnConnectedToServer.hasOptionalOverride = true;

                var OnControllerColliderHit = MethodOverride("OnControllerColliderHit", AccessModifier.Private, MethodModifier.None, typeof(void), "OnControllerColliderHit", new(string name, Type type)[] { }, true);
                if (OnControllerColliderHit != null) OnControllerColliderHit.hasOptionalOverride = true;

                var OnDrawGizmos = MethodOverride("OnDrawGizmos", AccessModifier.Private, MethodModifier.None, typeof(void), "OnDrawGizmos", new(string name, Type type)[] { }, true);
                if (OnDrawGizmos != null) OnDrawGizmos.hasOptionalOverride = true;

                var OnDrawGizmosSelected = MethodOverride("OnDrawGizmosSelected", AccessModifier.Private, MethodModifier.None, typeof(void), "OnDrawGizmosSelected", new(string name, Type type)[] { }, true);
                if (OnDrawGizmosSelected != null) OnDrawGizmosSelected.hasOptionalOverride = true;

                var OnJointBreak = MethodOverride("OnJointBreak", AccessModifier.Private, MethodModifier.None, typeof(void), "OnJointBreak", new(string name, Type type)[] { ("breakForce", typeof(float)) }, true);
                if (OnJointBreak != null) OnJointBreak.hasOptionalOverride = true;

                var OnJointBreak2D = MethodOverride("OnJointBreak2D", AccessModifier.Private, MethodModifier.None, typeof(void), "OnJointBreak2D", new(string name, Type type)[] { ("brokenJoint", typeof(Joint2D)) }, true);
                if (OnJointBreak2D != null) OnJointBreak2D.hasOptionalOverride = true;

                var OnMouseDown = MethodOverride("OnMouseDown", AccessModifier.Private, MethodModifier.None, typeof(void), "OnMouseDown", new(string name, Type type)[] { }, true);
                if (OnMouseDown != null) OnMouseDown.hasOptionalOverride = true;

                var OnMouseDrag = MethodOverride("OnMouseDrag", AccessModifier.Private, MethodModifier.None, typeof(void), "OnMouseDrag", new(string name, Type type)[] { }, true);
                if (OnMouseDrag != null) OnMouseDrag.hasOptionalOverride = true;

                var OnMouseEnter = MethodOverride("OnMouseEnter", AccessModifier.Private, MethodModifier.None, typeof(void), "OnMouseEnter", new(string name, Type type)[] { }, true);
                if (OnMouseEnter != null) OnMouseEnter.hasOptionalOverride = true;

                var OnMouseExit = MethodOverride("OnMouseExit", AccessModifier.Private, MethodModifier.None, typeof(void), "OnMouseExit", new(string name, Type type)[] { }, true);
                if (OnMouseExit != null) OnMouseExit.hasOptionalOverride = true;

                var OnMouseOver = MethodOverride("OnMouseOver", AccessModifier.Private, MethodModifier.None, typeof(void), "OnMouseOver", new(string name, Type type)[] { }, true);
                if (OnMouseOver != null) OnMouseOver.hasOptionalOverride = true;

                var OnMouseUp = MethodOverride("OnMouseUp", AccessModifier.Private, MethodModifier.None, typeof(void), "OnMouseUp", new(string name, Type type)[] { }, true);
                if (OnMouseUp != null) OnMouseUp.hasOptionalOverride = true;

                var OnMouseUpAsButton = MethodOverride("OnMouseUpAsButton", AccessModifier.Private, MethodModifier.None, typeof(void), "OnMouseUpAsButton", new(string name, Type type)[] { }, true);
                if (OnMouseUpAsButton != null) OnMouseUpAsButton.hasOptionalOverride = true;

                var OnParticleCollision = MethodOverride("OnParticleCollision", AccessModifier.Private, MethodModifier.None, typeof(void), "OnParticleCollision", new(string name, Type type)[] { ("other", typeof(GameObject)) }, true);
                if (OnParticleCollision != null) OnParticleCollision.hasOptionalOverride = true;

                var OnParticleSystemStopped = MethodOverride("OnParticleSystemStopped", AccessModifier.Private, MethodModifier.None, typeof(void), "OnParticleSystemStopped", new(string name, Type type)[] { }, true);
                if (OnParticleSystemStopped != null) OnParticleSystemStopped.hasOptionalOverride = true;

                var OnParticleTrigger = MethodOverride("OnParticleTrigger", AccessModifier.Private, MethodModifier.None, typeof(void), "OnParticleTrigger", new(string name, Type type)[] { }, true);
                if (OnParticleTrigger != null) OnParticleTrigger.hasOptionalOverride = true;

                var OnParticleUpdateJobScheduled = MethodOverride("OnParticleUpdateJobScheduled", AccessModifier.Private, MethodModifier.None, typeof(void), "OnParticleUpdateJobScheduled", new(string name, Type type)[] { }, true);
                if (OnParticleUpdateJobScheduled != null) OnParticleUpdateJobScheduled.hasOptionalOverride = true;

                var OnPostRender = MethodOverride("OnPostRender", AccessModifier.Private, MethodModifier.None, typeof(void), "OnPostRender", new(string name, Type type)[] { }, true);
                if (OnPostRender != null) OnPostRender.hasOptionalOverride = true;

                var OnPreCull = MethodOverride("OnPreCull", AccessModifier.Private, MethodModifier.None, typeof(void), "OnPreCull", new(string name, Type type)[] { }, true);
                if (OnPreCull != null) OnPreCull.hasOptionalOverride = true;

                var OnPreRender = MethodOverride("OnPreRender", AccessModifier.Private, MethodModifier.None, typeof(void), "OnPreRender", new(string name, Type type)[] { }, true);
                if (OnPreRender != null) OnPreRender.hasOptionalOverride = true;

                var OnRenderImage = MethodOverride("OnRenderImage", AccessModifier.Private, MethodModifier.None, typeof(void), "OnRenderImage", new(string name, Type type)[] { ("src", typeof(RenderTexture)), ("dest", typeof(RenderTexture)) }, true);
                if (OnRenderImage != null) OnRenderImage.hasOptionalOverride = true;

                var OnRenderObject = MethodOverride("OnRenderObject", AccessModifier.Private, MethodModifier.None, typeof(void), "OnRenderObject", new(string name, Type type)[] { }, true);
                if (OnRenderObject != null) OnRenderObject.hasOptionalOverride = true;

                var OnTransformChildrenChanged = MethodOverride("OnTransformChildrenChanged", AccessModifier.Private, MethodModifier.None, typeof(void), "OnTransformChildrenChanged", new(string name, Type type)[] { }, true);
                if (OnTransformChildrenChanged != null) OnTransformChildrenChanged.hasOptionalOverride = true;

                var OnTransformParentChanged = MethodOverride("OnTransformParentChanged", AccessModifier.Private, MethodModifier.None, typeof(void), "OnTransformParentChanged", new(string name, Type type)[] { }, true);
                if (OnTransformParentChanged != null) OnTransformParentChanged.hasOptionalOverride = true;

                var OnTriggerEnter = MethodOverride("OnTriggerEnter", AccessModifier.Private, MethodModifier.None, typeof(void), "OnTriggerEnter", new(string name, Type type)[] { ("collider", typeof(Collider)) }, true);
                if (OnTriggerEnter != null) OnTriggerEnter.hasOptionalOverride = true;

                var OnTriggerEnter2D = MethodOverride("OnTriggerEnter2D", AccessModifier.Private, MethodModifier.None, typeof(void), "OnTriggerEnter2D", new(string name, Type type)[] { ("collider", typeof(Collider2D)) }, true);
                if (OnTriggerEnter2D != null) OnTriggerEnter2D.hasOptionalOverride = true;

                var OnTriggerExit = MethodOverride("OnTriggerExit", AccessModifier.Private, MethodModifier.None, typeof(void), "OnTriggerExit", new(string name, Type type)[] { ("collider", typeof(Collider)) }, true);
                if (OnTriggerExit != null) OnTriggerExit.hasOptionalOverride = true;

                var OnTriggerExit2D = MethodOverride("OnTriggerExit2D", AccessModifier.Private, MethodModifier.None, typeof(void), "OnTriggerExit2D", new(string name, Type type)[] { ("collider", typeof(Collider2D)) }, true);
                if (OnTriggerExit2D != null) OnTriggerExit2D.hasOptionalOverride = true;

                var OnTriggerStay = MethodOverride("OnTriggerStay", AccessModifier.Private, MethodModifier.None, typeof(void), "OnTriggerStay", new(string name, Type type)[] { ("collider", typeof(Collider)) }, true);
                if (OnTriggerStay != null) OnTriggerStay.hasOptionalOverride = true;

                var OnTriggerStay2D = MethodOverride("OnTriggerStay2D", AccessModifier.Private, MethodModifier.None, typeof(void), "OnTriggerStay2D", new(string name, Type type)[] { ("collider", typeof(Collider2D)) }, true);
                if (OnTriggerStay2D != null) OnTriggerStay2D.hasOptionalOverride = true;

                var OnValidate = MethodOverride("OnValidate", AccessModifier.Private, MethodModifier.None, typeof(void), "OnValidate", new(string name, Type type)[] { }, true);
                if (OnValidate != null) OnValidate.hasOptionalOverride = true;

                var OnWillRenderObject = MethodOverride("OnWillRenderObject", AccessModifier.Private, MethodModifier.None, typeof(void), "OnWillRenderObject", new(string name, Type type)[] { }, true);
                if (OnWillRenderObject != null) OnWillRenderObject.hasOptionalOverride = true;

                var Reset = MethodOverride("Reset", AccessModifier.Private, MethodModifier.None, typeof(void), "Reset", new(string name, Type type)[] { }, true);
                if (Reset != null) Reset.hasOptionalOverride = true;

                return true;
            }

            return false;
        }

        private bool ScriptableObjects()
        {
            if (inherits.Inherits<ScriptableObject>())
            {
                var Awake = MethodOverride("Awake", AccessModifier.Private, MethodModifier.None, typeof(void), "Awake", new(string name, Type type)[] { }, true);
                if (Awake != null) Awake.hasOptionalOverride = true;

                var OnEnable = MethodOverride("OnEnable", AccessModifier.Private, MethodModifier.None, typeof(void), "OnEnable", new(string name, Type type)[] { }, true);
                if (OnEnable != null) OnEnable.hasOptionalOverride = true;

                var OnDisable = MethodOverride("OnDisable", AccessModifier.Private, MethodModifier.None, typeof(void), "OnDisable", new(string name, Type type)[] { }, true);
                if (OnDisable != null) OnDisable.hasOptionalOverride = true;

                var OnDestroy = MethodOverride("OnDestroy", AccessModifier.Private, MethodModifier.None, typeof(void), "OnDestroy", new(string name, Type type)[] { }, true);
                if (OnDestroy != null) OnDestroy.hasOptionalOverride = true;

                return true;
            }

            return false;
        }

#if UNITY_EDITOR
        private bool EditorWindows()
        {
            if (inherits.Inherits<EditorWindow>())
            {
                var Awake = MethodOverride("Awake", AccessModifier.Private, MethodModifier.None, typeof(void), "Awake", new(string name, Type type)[] { }, true);
                if (Awake != null) Awake.hasOptionalOverride = true;

                var Update = MethodOverride("Update", AccessModifier.Private, MethodModifier.None, typeof(void), "Update", new(string name, Type type)[] { }, true);
                if (Update != null) Update.hasOptionalOverride = true;

                var OnDestroy = MethodOverride("OnDestroy", AccessModifier.Private, MethodModifier.None, typeof(void), "OnDestroy", new(string name, Type type)[] { }, true);
                if (OnDestroy != null) OnDestroy.hasOptionalOverride = true;

                var OnEnable = MethodOverride("OnFocus", AccessModifier.Private, MethodModifier.None, typeof(void), "OnEnable", new(string name, Type type)[] { }, true);
                if (OnEnable != null) OnEnable.hasOptionalOverride = true;

                var OnGUI = MethodOverride("OnGUI", AccessModifier.Private, MethodModifier.None, typeof(void), "OnGUI", new(string name, Type type)[] { }, true);
                if (OnGUI != null) OnGUI.hasOptionalOverride = true;

                var OnHierarchyChange = MethodOverride("OnHierarchyChange", AccessModifier.Private, MethodModifier.None, typeof(void), "OnHierarchyChange", new(string name, Type type)[] { }, true);
                if (OnHierarchyChange != null) OnHierarchyChange.hasOptionalOverride = true;

                var OnInspectorUpdate = MethodOverride("OnInspectorUpdate", AccessModifier.Private, MethodModifier.None, typeof(void), "OnInspectorUpdate", new(string name, Type type)[] { }, true);
                if (OnInspectorUpdate != null) OnInspectorUpdate.hasOptionalOverride = true;

                var OnLostFocus = MethodOverride("OnLostFocus", AccessModifier.Private, MethodModifier.None, typeof(void), "OnLostFocus", new(string name, Type type)[] { }, true);
                if (OnLostFocus != null) OnLostFocus.hasOptionalOverride = true;

                var OnProjectChange = MethodOverride("OnProjectChange", AccessModifier.Private, MethodModifier.None, typeof(void), "OnProjectChange", new(string name, Type type)[] { }, true);
                if (OnProjectChange != null) OnProjectChange.hasOptionalOverride = true;

                var OnSelectionChange = MethodOverride("OnSelectionChange", AccessModifier.Private, MethodModifier.None, typeof(void), "OnSelectionChange", new(string name, Type type)[] { }, true);
                if (OnSelectionChange != null) OnSelectionChange.hasOptionalOverride = true;

                return true;
            }

            return false;
        }


        private void RemoveUnusedMethods()
        {
            var removeAmount = 10;

            for (int i = 0; i < removeAmount; i++)
            {
                if (i > 0 && !removedMethod) break;

                overrideMethods.Undefine(lastDefinedOverrideMethods, (method) =>
                {
                    UnityEngine.Object.DestroyImmediate(method.macro, true);
                    removedMethod = true;
                });
            }

            if (removedMethod)
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
#endif
    }
}