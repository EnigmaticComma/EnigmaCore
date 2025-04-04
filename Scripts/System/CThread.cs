﻿using System.Threading;
using UnityEngine;

namespace EnigmaCore {
    public static class CThread {

        public static Thread MainThread { get; private set; } 
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void InitializeBeforeSplashScreen() {
            MainThread = Thread.CurrentThread;
        }
        
    }
}