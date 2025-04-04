﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace EnigmaCore {
    public class ETimedAutoTrigger : CAutoTriggerCompBase {

        [SerializeField, Min(0f)] float _secondsToTrigger = 5f;
        [SerializeField] UnityEvent _onTriggerEvent;




        protected override void TriggerEvent() {
            this.CStartCoroutine(TriggerRoutine());
        }

        IEnumerator TriggerRoutine() {
            yield return new WaitForSeconds(_secondsToTrigger);
            _onTriggerEvent?.Invoke();
        }

    }
}