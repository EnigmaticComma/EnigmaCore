using System;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if NEWTONSOFT_JSON
using Newtonsoft.Json;
#endif

#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

namespace EnigmaCore {
	[JsonObject(MemberSerialization.OptIn)]
	public abstract class CPersistentData {

		#region <<---------- Saving ---------->>
        
        protected static bool SaveJsonTextToFile(string json, string filePath) {
			try {
                using (var streamWriter = File.CreateText(filePath)) {
                    streamWriter.Write(json);
                }
                return true;
            }
			catch (Exception e) {
				Debug.LogError(e);
                return false;
            }
		}
		
		#endregion <<---------- Saving ---------->>




		#region <<---------- Path ---------->>

        public static string GetApplicationPersistentDataFolder() {
            string path = null;
            
            #if UNITY_WEBGL
            path = Path.Combine(Application.persistentDataPath, "idbfs");
            #else
            path = Application.persistentDataPath;
            #endif

            if (Debug.isDebugBuild) {
                Debug.Log($"Application.{nameof(Application.persistentDataPath)}: '{Application.persistentDataPath}'");
            }
            return path;
        }

        #endregion <<---------- Path ---------->>


        
        
        #region <<---------- JS External Invocation ---------->>
#if UNITY_WEBGL
        [DllImport("__Internal")]
        protected static extern void WebglSyncFiles();
#endif
        #endregion <<---------- JS External Invocation ---------->>


        
        
		#region <<---------- Editor ---------->>
	
		#if UNITY_EDITOR

		[MenuItem("Tools/Open root save folder")]
		public static void OpenSaveFolder() {
			EditorUtility.RevealInFinder(GetApplicationPersistentDataFolder());
		}
		
		#endif
		
		#endregion <<---------- Editor ---------->>
		
	}
}