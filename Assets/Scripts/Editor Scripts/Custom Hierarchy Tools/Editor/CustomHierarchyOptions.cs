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
			DrawActiveToggleButton(instanceID, selectionRect);
		}

		private static Rect DrawRect(float x, float y, float size) {
			return new Rect(x, y, size, size);
		}

		private static void DrawActiveToggleButton(int id, Rect selectionRect) {
			DrawButtonWithToggle(id, selectionRect.x - 20, selectionRect.y + 3, 10);
		}

		private static void DrawButtonWithToggle(int id, float x, float y, float size) {
			GameObject gameObject = EditorUtility.InstanceIDToObject(id) as GameObject;
			if (gameObject == null) return;

			Rect rect = DrawRect(x, y, size);
			gameObject.SetActive(GUI.Toggle(rect, gameObject.activeSelf, string.Empty));
		}
	}
}
#endif