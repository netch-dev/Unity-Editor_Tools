using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PopupExample : EditorWindow {
	private int selectedOptionIndex;

	[MenuItem("Window/Popup Example")]
	public static void ShowWindow() {
		EditorWindow window = GetWindow(typeof(PopupExample));
		window.Show();
	}

	private void OnGUI() {
		selectedOptionIndex = EditorGUILayout.Popup(selectedOptionIndex, new string[] { "Option 1", "Option 2", "Option 3" });
	}
}
