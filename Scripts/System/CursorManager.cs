using System;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace EnigmaCore {
	public class CursorManager
	{
		[NonSerialized] readonly CBlockingEventsManager _blockingEventsManager;

		public CursorManager(CBlockingEventsManager blockingEventsManager) {
	        _blockingEventsManager = blockingEventsManager;

	        _blockingEventsManager.MenuRetainable.StateEvent += (onMenu) => {
                if (!onMenu) {
                    SetCursorState(false);
                    return;
                }
                ShowMouseIfNeeded();
            };

            EApplication.QuittingEvent += OnAppQuitting;
        }
		
		void OnAppQuitting()
		{
			EApplication.QuittingEvent -= OnAppQuitting;
			#if UNITY_EDITOR
			SetCursorState(true);
			#endif
		}

		static void SetCursorState(bool visible)
		{
			Cursor.visible = visible;
			Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
		}

        public void ShowMouseIfNeeded() {
#if ENABLE_INPUT_SYSTEM
            if (Gamepad.current != null && Gamepad.current.enabled) return;
#endif
            SetCursorState(true);
        }

		public void HideCursor() {
            SetCursorState(false);
        }

	}
}