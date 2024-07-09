using UnityEditor;
using UnityEngine;

public class MissingReferenceDetector : EditorWindow {
	[MenuItem("Custom Tools/Find Missing References")]
	public static void ShowWindow() {
		EditorWindow window = GetWindow(typeof(MissingReferenceDetector));
		window.maxSize = new Vector2(250, 100);
		window.minSize = window.maxSize;

		GUIContent guiContent = new GUIContent("Find Missing References");
		window.titleContent = guiContent;
		window.Show();
	}

	private void OnGUI() {
		EditorGUILayout.Space();
		if (GUILayout.Button("Find Missing References")) {
			FindMissingReferences();
		}

		EditorGUILayout.Space();
		Repaint();
	}

	private void FindMissingReferences() {
		int missingReferenceCount = 0;
		GameObject[] gameObjects = FindObjectsOfType<GameObject>();
		foreach (GameObject gameObject in gameObjects) {
			Component[] components = gameObject.GetComponents<Component>();
			foreach (Component component in components) {
				SerializedObject serializedObject = new SerializedObject(component);
				SerializedProperty serializedProperty = serializedObject.GetIterator();
				while (serializedProperty.NextVisible(true)) {
					if (serializedProperty.propertyType == SerializedPropertyType.ObjectReference) {
						if (serializedProperty.objectReferenceValue == null) {
							string message = "<color=red><b>Missing reference: </b></color> " + gameObject.name + " - Property: " + serializedProperty.displayName;
							Debug.Log(message, gameObject);
							missingReferenceCount++;
						}
					}
				}
			}
		}

		if (missingReferenceCount == 0) {
			Debug.Log("<color=green><b>No missing references found!</b></color>");
		} else {
			Debug.Log("<color=red><b>Found " + missingReferenceCount + " missing references!</b></color>");
		}
	}
}
