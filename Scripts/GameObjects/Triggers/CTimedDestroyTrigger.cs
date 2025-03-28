﻿using UnityEngine;

namespace EnigmaCore {
	public class ETimedDestroyTrigger : CAutoTriggerCompBase {
		[SerializeField][Range(0f, 600f)] float _secondsToDestroy = 5f;
        [SerializeField] bool _shouldLog = true;
		
		protected override void TriggerEvent() {
			gameObject.CDestroy(_shouldLog, _secondsToDestroy);
		}
	}
}