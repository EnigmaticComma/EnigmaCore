﻿using UnityEngine;

namespace EnigmaCore {
	public static class CLayerMaskExtensions {
		public static bool CContains(this LayerMask self, LayerMask anotherLayer) {
			return (self & (1 << anotherLayer)) != 0;
		}
    }
}
