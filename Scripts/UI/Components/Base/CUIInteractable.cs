using System;
using EnigmaCore.DependecyInjection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if FMOD
using FMODUnity;
using FMOD.Studio;
using STOP_MODE = FMOD.Studio.STOP_MODE;
#endif

namespace EnigmaCore.UI {
	public class CUIInteractable : MonoBehaviour, ISelectHandler, ISubmitHandler, ICancelHandler,
	                               IPointerEnterHandler, IPointerClickHandler, IDeselectHandler {
       
        #region <<---------- Properties and Fields ---------->>

        [SerializeField] bool _debug;
        [SerializeField] bool _playInteractionSound = true;
        
		#if FMOD
		EventInstance _soundEventInstance;
		#endif

        [SerializeField] UnityEvent _interactEvent;

        #endregion <<---------- Properties and Fields ---------->>


        
        
        #region <<---------- Mono Behaviour ---------->>

        protected virtual void Awake() { }

        protected virtual void OnEnable() { }

        #endregion <<---------- Mono Behaviour ---------->>


        protected void TryEndNavigation()
        {
	        if(transform.root == null || !transform.root.TryGetComponent(out View view)) {
		        Debug.LogError("Could not get View from root to end navigation", this);
		        return;
	        }
	        view.RecursiveCloseAllViews();
        }

		#if FMOD
		void PlaySound(EventReference sound) {
			try {
				if (_playInteractionSound && !sound.IsNull) {
					_soundEventInstance.stop(STOP_MODE.IMMEDIATE);
					_soundEventInstance = RuntimeManager.CreateInstance(sound);
					_soundEventInstance.start();
				}
			}
			catch (Exception e) {
				Debug.LogError($"Error trying to PlaySound '{sound}': {e.Message}\n{e}");
			}
		}
		#endif

		bool IsThisAValidInteractionTarget(BaseEventData eventData)
		{
			if (!gameObject.activeInHierarchy) return false;
			if (eventData is PointerEventData pointerEventData) {
				if (pointerEventData.pointerEnter != gameObject) return false;
			}
			return true;
		}

		public virtual void Selected(bool playSound = true) {
			if(_debug) Debug.Log($"Selected: CUIInteractable '{gameObject.name}'", this);
			#if FMOD
			if(playSound) PlaySound(DIContainer.Resolve<UISoundsBankSO>().SoundSelect);
			#endif
		}

		public virtual void Submited() {
			_interactEvent?.Invoke();
			if(_debug) Debug.Log($"SUBMIT: CUIInteractable '{gameObject.name}'", this);
			#if FMOD
            if (!(this is CUIButton b && !b.Button.interactable)) {
                PlaySound(DIContainer.Resolve<UISoundsBankSO>().SoundSubmit);
            }
			#endif
		}

		public virtual void Canceled() {
			if(_debug) Debug.Log($"CANCEL: CUIInteractable '{gameObject.name}'", this);
			#if FMOD
			PlaySound(DIContainer.Resolve<UISoundsBankSO>().SoundCancel);
			#endif
		}
		
		#region <<---------- IHandlers ---------->>
		
		public void OnSelect(BaseEventData eventData)
		{
			if (!IsThisAValidInteractionTarget(eventData)) return;
			Selected();
		}

		public void OnSubmit(BaseEventData eventData) {
			if (!IsThisAValidInteractionTarget(eventData)) return;
			Submited();
		}
		
		public void OnCancel(BaseEventData eventData) {
			if (!IsThisAValidInteractionTarget(eventData)) return;
			var rootT = transform.root;
            if (rootT != null && rootT.TryGetComponent(out View view)) {
	            view.CloseView();
                Canceled();
            }
        }

		// Pointer
		public virtual void OnPointerEnter(PointerEventData eventData) {
			if (!IsThisAValidInteractionTarget(eventData)) return;
			var selectable = GetComponent<Selectable>();
			if (selectable == null) return;
			selectable.Select();
			Selected(false);
		}
        
        public virtual void OnDeselect(BaseEventData eventData) {
            
        }
		
		public void OnPointerClick(PointerEventData eventData) {
			if (!IsThisAValidInteractionTarget(eventData)) return;
            if (eventData.button == PointerEventData.InputButton.Right) return;
            Submited();
		}
		
		#endregion <<---------- IHandlers ---------->>
        
    }
}
