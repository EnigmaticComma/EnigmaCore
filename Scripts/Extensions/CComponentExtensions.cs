﻿using EnigmaCore.DependecyInjection;
using Unity.Linq;
using UnityEngine;

namespace EnigmaCore {
    public static class CComponentExtensions {

        public static void Inject(this Component c) {
            DIContainer.InjectDependencies(c);
        }

        public static T CGetComponentInChildrenOrInParent<T>(this Component comp, bool includeInactive = false) {
            return comp.gameObject.CGetComponentInChildrenOrInParent<T>(includeInactive);
        }
        
        public static T CGetComponentInParentOrInChildren<T>(this Component comp, bool includeInactive = false) {
            return comp.gameObject.CGetComponentInParentOrInChildren<T>(includeInactive);

        }
        
        public static T CGetComponentInChildrenRecursiveUntilRoot<T>(this Component comp, bool includeInactive = false) {
            if (comp == null || comp.gameObject == null) return default;
            T target = default;
            foreach (var ancestor in comp.gameObject.AncestorsAndSelf()) {
                target = ancestor.GetComponentInChildren<T>(includeInactive);
                if (target != null) {
                    return target;
                }
            }
            return target;
        }
        
        public static void CDestroyGameObject(this Component value, float time = 0f) {
            if (value == null || value.gameObject == null) return;
            if (Application.isPlaying) {
                Object.Destroy(value.gameObject, time > 0f ? time : 0f);
            }
            else {
                Object.DestroyImmediate(value.gameObject);
            }
        }

        public static T CGetOrAddComponent<T>(this Component value) where T : Component {
            if (value == null) {
                Debug.LogError($"Cant add component to a null component!");
                return default;
            }

            var comp = value.GetComponent<T>();
            return (comp != null ? comp : value.gameObject.AddComponent<T>());
        }
        
        public static bool CAssertIfNull<T>(this T c, string message = null, Object source = null) where T : Component {
            bool isNull = (c == null);
            if (isNull) {
                Debug.LogError($"<b>Assert</b>: Component is null ({message})", source);
            }
            return isNull;
        }

        public static void CDestroyGameObject<T>(this T value, bool shouldLog = false, float time = 0f) where T : Component {
            if (value == null) return;
            value.gameObject.CDestroy(shouldLog, time);
        }

    }
}