using System;
using EnigmaCore.DependecyInjection;
using EnigmaCore.UI;
using UnityEngine;

namespace EnigmaCore.Interaction {
	public class CInteractable : MonoBehaviour, ICInteractable {

		#region <<---------- Properties and Fields ---------->>
		
		[NonSerialized,Inject] CBlockingEventsManager _blockingEventsManager;
        [SerializeField] protected bool _debug;
		[SerializeField] bool onlyWorkOneTimePerSceneLoad;
        [SerializeField] Transform _interactionPromptPoint;
		[SerializeField] CUnityEventTransform InteractEvent;

		#endregion <<---------- Properties and Fields ---------->>


		
		
		#region <<---------- MonoBehaviour ---------->>

		protected virtual void Awake()
		{
			this.Inject();
		}

		protected virtual void OnEnable() {
			// show enable checkbox
		}
        
		#endregion <<---------- MonoBehaviour ---------->>


		
		
		#region <<---------- CIInteractable ---------->>

        public virtual bool CanBeInteractedWith() {
            return this != null && enabled;
        }

        /// <summary>
        /// Returns TRUE if interacted sucesfull.
        /// </summary>
		public virtual bool OnInteract(Transform interactingTransform) {
			if (!enabled || gameObject == null || !gameObject.activeInHierarchy || _blockingEventsManager.InMenuOrPlayingCutscene) return false;
			InteractEvent?.Invoke(interactingTransform);
			if (onlyWorkOneTimePerSceneLoad) {
				Destroy(this);
			}
            return true;
        }

        public virtual void OnBecameInteractionTarget(Transform lookingTransform) { }

        public virtual void OnStoppedBeingInteractionTarget(Transform lookingTransform) { }

        public Vector3 GetInteractionPromptPoint() {
            if(this == null)return Vector3.zero;
            return _interactionPromptPoint != null ? _interactionPromptPoint.position : transform.position;
        }

        #endregion <<---------- CIInteractable ---------->>

    }
}