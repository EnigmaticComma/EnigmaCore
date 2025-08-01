﻿using UnityEngine;

namespace EnigmaCore {
    public static class CSingletonHelper {
        
        public static T CreateInstance<T>(string gameObjectName) where T : MonoBehaviour {
            if (CannotCreateAnyInstance()) {
                return null;
            }
            return new GameObject(gameObjectName).CDontDestroyOnLoad().AddComponent<T>();
        }
        
        public static bool CannotCreateAnyInstance() {
            return EApplication.IsQuitting || !Application.isPlaying;
        }

    }
}