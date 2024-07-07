using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace Netch.UtilityScripts {
	[InitializeOnLoad]
	public class CustomHierarchyOptions {
		static CustomHierarchyOptions() {
			EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
		}

		private static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect) {

		}
	}
}
#endif