﻿using System;
using UnityEngine;

namespace EnigmaCore.Renderers {
    [ExecuteInEditMode]
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    public class CRealtimeCopyBlendShapeValues : MonoBehaviour {

        [SerializeField] SkinnedMeshRenderer _sourceMeshRenderer;
        SkinnedMeshRenderer _targetMeshRenderer;
        [SerializeField] uint _timeSplit = 2;


        void Awake() {
            TryGetComponent(out _targetMeshRenderer);
        }

        private void LateUpdate() {
            if (_timeSplit > 0 && Time.frameCount % _timeSplit != 0) return;
            if(_sourceMeshRenderer == null || _targetMeshRenderer == null) return;
            CopyBlendShapes(_sourceMeshRenderer, _targetMeshRenderer);
        }

        private void Reset() {
            if (_targetMeshRenderer == null) {
                if (this.TryGetComponent(out SkinnedMeshRenderer skinnedMeshRenderer)) {
                    _targetMeshRenderer = skinnedMeshRenderer;
                    #if UNITY_EDITOR
                    UnityEditor.EditorUtility.SetDirty(this);
                    #endif
                }
            }
        }

        public static void CopyBlendShapes(SkinnedMeshRenderer source, SkinnedMeshRenderer target) {
            if (source.sharedMesh.blendShapeCount != target.sharedMesh.blendShapeCount) return;
            for (int i = 0; i < source.sharedMesh.blendShapeCount; i++) {
                var name = source.sharedMesh.GetBlendShapeName(i);
                var index = target.sharedMesh.GetBlendShapeIndex(name);
                if (index == -1) continue;
                var value = source.GetBlendShapeWeight(i);
                target.SetBlendShapeWeight(index, value);
            }
        }

    }
}