using UnityEditor;
using UnityEngine;

public class ObjectFieldExample : EditorWindow {
	private Object obj;
	[MenuItem("Window/Object Field Example")]
	public static void ShowWindow() {
		EditorWindow window = GetWindow(typeof(ObjectFieldExample));
		window.Show();
	}

	private void OnGUI() {
		obj = EditorGUILayout.ObjectField(obj, typeof(GameObject), true);
	}
}
