﻿using UnityEngine;

namespace EnigmaCore {
    public class CFixedWorldRotation : MonoBehaviourUpdateExecutionLoopTime {

        [SerializeField] private Vector3 _axis = new Vector3(90f,0f,0f);
        
        
        protected override void Execute(float deltaTime) {
            this.transform.rotation = Quaternion.Euler(this._axis);
        }
    }
}