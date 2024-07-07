using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace Netch.UtilityScripts {
	public class EditorScriptLifecycle : EditorWindow {
		[MenuItem("Window/Example Script Lifecycle")]
		public static void ShowWindow() {
			EditorWindow editorWindow = GetWindow(typeof(EditorScriptLifecycle));
			editorWindow.Show();
		}

		public void Awake() {
			// Awake is called when the editor window is opened
			Debug.Log("Awake");
		}

		public void CreateGUI() {
			// Called when the editor windows root visual element is ready to be populated
			Debug.Log("CreateGUI");
		}

		public void OnBecameVisible() {
			// Called after the window is added to a container view
			Debug.Log("OnBecameVisible");
		}

		private void OnFocus() {
			// Called when the window receives keyboard focus
			Debug.Log("OnFocus");
		}

		private void OnGUI() {
			// Called multiple times per second to draw the GUI window
			Debug.Log("OnGUI");
		}

		private void OnHierarchyChange() {
			// Called whenever the hierarchy changes
			Debug.Log("OnHierarchyChange");
		}

		private void OnInspectorUpdate() {
			// Called 10 times per second
			Debug.Log("OnInspectorUpdate");
		}

		private void OnProjectChange() {
			// Called whenever the state of the project changes
			Debug.Log("OnProjectChange");
		}

		private void OnSelectionChange() {
			// Called whenever the selection changes
			Debug.Log("OnSelectionChange");
		}

		private void Update() {
			// Called once per frame, also accessable in editor scripts
			Debug.Log("Update");
		}

		private void OnLostFocus() {
			// Called when the window loses keyboard focus
			Debug.Log("OnLostFocus");
		}

		private void OnBecameInvisible() {
			// Called after the window is removed from a container view, or is no longer visible within a tabbed collection of editor windows
			Debug.Log("OnBecameInvisible");
		}

		private void OnDestroy() {
			// Called when the window is destroyed
			Debug.Log("OnDestroy");
		}
	}
}
#endif
