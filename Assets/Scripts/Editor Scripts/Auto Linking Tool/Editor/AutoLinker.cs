using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AutoLinker : Editor {
	private static Dictionary<string, GameObject> hierarchyNameToGameObjectMap;
	private static Dictionary<string, SerializedObject> inspectorFieldNameToSerializedPropertyMap;

	[InitializeOnLoadMethod]
	private static void Setup() {
		SetupHierarchyMap();
		SetupInspectorMap();
		HandleAutoLinking();
	}

	private static void SetupHierarchyMap() {
		hierarchyNameToGameObjectMap = new Dictionary<string, GameObject>();

		GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();
		foreach (GameObject gameObject in gameObjects) {
			string key = gameObject.name.ToLower().Replace(" ", "");
			hierarchyNameToGameObjectMap.Add(key, gameObject);
		}
	}

	private static void SetupInspectorMap() {
		inspectorFieldNameToSerializedPropertyMap = new Dictionary<string, SerializedObject>();

		foreach (GameObject gameObject in hierarchyNameToGameObjectMap.Values) {
			Component[] components = gameObject.GetComponents<Component>();
			foreach (Component component in components) {
				SerializedObject serializedObject = new SerializedObject(component);
				SerializedProperty property = serializedObject.GetIterator();
				while (property.NextVisible(true)) {
					if (property.propertyType == SerializedPropertyType.ObjectReference) {
						string key = property.displayName.ToLower().Replace(" ", "");
						if (!inspectorFieldNameToSerializedPropertyMap.ContainsKey(key)) {
							inspectorFieldNameToSerializedPropertyMap.Add(key, serializedObject);
						}
					}
				}
			}
		}
	}

	private static void HandleAutoLinking() {
		foreach (string name in inspectorFieldNameToSerializedPropertyMap.Keys) {
			string key = name.ToLower().Replace(" ", "");
			if (hierarchyNameToGameObjectMap.ContainsKey(key)) {
				SerializedProperty serializedProperty = inspectorFieldNameToSerializedPropertyMap[key].FindProperty(key);
				serializedProperty.objectReferenceValue = hierarchyNameToGameObjectMap[key];
				serializedProperty.serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
