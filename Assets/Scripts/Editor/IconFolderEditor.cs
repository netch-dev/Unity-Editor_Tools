using System;
using UnityEditor;
using UnityEngine;

// To select an icon for the folder use the object picker:
// https://docs.unity3d.com/ScriptReference/EditorGUIUtility.ShowObjectPicker.html

#if UNITY_EDITOR
namespace Netch.UtilityScripts {
	[InitializeOnLoad] // Attribute to edit folders whenever we load unity or anything changes
	internal static class IconFolderEditor {
		private static string selectedFolderGuid;
		private static int controlID;

		static IconFolderEditor() {
			EditorApplication.projectWindowItemOnGUI -= OnGUI;
			EditorApplication.projectWindowItemOnGUI += OnGUI;
		}

		// Use OnGUI to listen for the ShowObjectPicker event
		private static void OnGUI(string guid, Rect selectionRect) {
			if (guid != selectedFolderGuid) return;

			if (Event.current.commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() == controlID) {
				UnityEngine.Object selectedObject = EditorGUIUtility.GetObjectPickerObject();

				string selectedObjectPath = AssetDatabase.GetAssetPath(selectedObject);
				string folderTextureGuid = AssetDatabase.AssetPathToGUID(selectedObjectPath);

				Debug.Log(folderTextureGuid);

				EditorPrefs.SetString(selectedFolderGuid, folderTextureGuid);
			}
		}

		public static void ChooseCustomIcon() {
			selectedFolderGuid = Selection.assetGUIDs[0];

			controlID = GUIUtility.GetControlID(FocusType.Passive);
			EditorGUIUtility.ShowObjectPicker<Sprite>(null, false, "", controlID);
		}
	}
}
#endif
