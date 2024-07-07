using UnityEngine;
using System;

// Probably don't need this editor check since it's in an Editor folder but I'll keep it for now
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

			AddInfoScriptToGameObject(instanceID);
			DrawInfoButton(instanceID, selectionRect, string.Empty);

			DrawZoomInButton(instanceID, selectionRect, "Focus this game object");

			DrawCreatePrefabButton(instanceID, selectionRect, "Save as prefab");
		}

		private static Rect DrawRect(float x, float y, float size) {
			return new Rect(x, y, size, size);
		}

		private static void DrawButtonWithTexture(float x, float y, float size, string name, Action action, GameObject gameObject, string tooltip) {
			if (gameObject == null) return;

			GUIStyle guiStyle = new GUIStyle();
			guiStyle.fixedHeight = 0;
			guiStyle.fixedWidth = 0;
			guiStyle.stretchHeight = true; // Stretch the full height and width of the button
			guiStyle.stretchWidth = true;

			Rect rect = DrawRect(x, y, size);
			Texture texture = Resources.Load(name) as Texture;

			GUIContent guiContent = new GUIContent();
			guiContent.image = texture;
			guiContent.text = "";
			guiContent.tooltip = tooltip;

			bool isClicked = GUI.Button(rect, guiContent, guiStyle);
			if (isClicked) action.Invoke();
		}

		#region Toggle Button
		private static void DrawActiveToggleButton(int id, Rect selectionRect) {
			DrawButtonWithToggle(id, selectionRect.x - 20, selectionRect.y + 3, 10);
		}

		private static void DrawButtonWithToggle(int id, float x, float y, float size) {
			GameObject gameObject = EditorUtility.InstanceIDToObject(id) as GameObject;
			if (gameObject == null) return;

			Rect rect = DrawRect(x, y, size);
			gameObject.SetActive(GUI.Toggle(rect, gameObject.activeSelf, string.Empty));
		}
		#endregion

		#region Info Button
		private static void DrawInfoButton(int id, Rect rect, string tooltip) {
			GameObject gameObject = EditorUtility.InstanceIDToObject(id) as GameObject;
			if (gameObject) {
				bool hasInfoScriptComponent = gameObject.GetComponent<Info>() != null;
				if (hasInfoScriptComponent) {
					Info infoScript = gameObject.GetComponent<Info>();
					tooltip = infoScript.info;
				}
			}

			DrawButtonWithTexture(rect.x + 150, rect.y + 2, 14, "info", () => { }, gameObject, tooltip);
		}

		private static void AddInfoScriptToGameObject(int id) {
			GameObject gameObject = EditorUtility.InstanceIDToObject(id) as GameObject;
			if (gameObject == null) return;

			bool hasInfoScriptComponent = gameObject.GetComponent<Info>() != null;
			if (!hasInfoScriptComponent) {
				gameObject.AddComponent<Info>();
			}
		}
		#endregion

		#region Zoom/Focus Button
		private static void DrawZoomInButton(int id, Rect rect, string tooltip) {
			GameObject gameObject = EditorUtility.InstanceIDToObject(id) as GameObject;
			if (gameObject == null) return;

			DrawButtonWithTexture(rect.x + 170, rect.y + 2, 14, "zoom_in", () => {
				Selection.activeGameObject = gameObject;
				SceneView.FrameLastActiveSceneView();
			}, gameObject, tooltip);
		}
		#endregion

		#region Create Prefab Button
		private static void DrawCreatePrefabButton(int id, Rect rect, string tooltip) {
			GameObject gameObject = EditorUtility.InstanceIDToObject(id) as GameObject;
			if (gameObject == null) return;

			DrawButtonWithTexture(rect.x + 190, rect.y + 2, 14, "prefab", () => {
				const string pathToPrefabsFolder = "Assets/Prefabs";
				bool doesPrefabsFolderExist = AssetDatabase.IsValidFolder(pathToPrefabsFolder);
				if (!doesPrefabsFolderExist) AssetDatabase.CreateFolder("Assets", "Prefabs");

				string prefabName = gameObject.name + ".prefab";
				string prefabPath = pathToPrefabsFolder + "/" + prefabName;
				AssetDatabase.DeleteAsset(prefabName);
				GameObject prefab = PrefabUtility.SaveAsPrefabAsset(gameObject, prefabPath);
				EditorGUIUtility.PingObject(prefab);
			}, gameObject, tooltip);
		}
		#endregion
	}
}
#endif