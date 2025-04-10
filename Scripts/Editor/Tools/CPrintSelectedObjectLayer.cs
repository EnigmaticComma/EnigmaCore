using UnityEditor;
using UnityEngine;

namespace EnigmaCore.Editor {
    public static class CPrintSelectedObjectLayer {
        [MenuItem("CONTEXT/Component/Print this object layer int value")]
        private static void RenameGameObjectWithThisComponentName(MenuCommand data) {
            if (!(data?.context is Component comp)) return;

            var go = comp.gameObject;
            if (go == null) return;

            Debug.Log($"{go.name} layer int value is {go.layer} and toString is {go.layer.ToString()}");
        }
    }
}