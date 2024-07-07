
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace Netch.UtilityScripts {
	public class IMGUI_Example : EditorWindow {
		[MenuItem("Window/Example")]
		public static void ShowWindow() {
			EditorWindow editorWindow = GetWindow(typeof(IMGUI_Example));
			editorWindow.Show();
		}

		private void OnGUI() {
			GUILayout.Label("Some example text...");
			EditorGUILayout.Space();
			GUILayout.Button("Button 1");
		}
	}
}
#endif