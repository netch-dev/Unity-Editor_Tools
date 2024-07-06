using System;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Netch.UtilityScripts {
	[InitializeOnLoad] // Attribute to edit folders whenever we load unity or anything changes
	internal static class ColouredFolderEditor {
		static ColouredFolderEditor() {
			EditorApplication.projectWindowItemOnGUI -= OnGUI;
			EditorApplication.projectWindowItemOnGUI += OnGUI;
		}

		private static void OnGUI(string guid, Rect selectionRect) {
			Rect folderRect;

			if (selectionRect.x < 15) {
				// Second column, small scale
				folderRect = new Rect(selectionRect.x + 3, selectionRect.y, selectionRect.height, selectionRect.height);
				//EditorGUI.DrawRect(selectionRect, Color.blue);

			} else if (selectionRect.x >= 15 && selectionRect.height < 30) {
				// First column
				folderRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.height, selectionRect.height);

			} else {
				// Second column, large scale
				folderRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width, selectionRect.width);
			}

			if (Selection.activeObject == null) return;
			string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
			string activeObjectGuid = AssetDatabase.GUIDFromAssetPath(assetPath).ToString();

			if (activeObjectGuid == guid) {
				EditorGUI.DrawRect(folderRect, Color.green);
			}

			//EditorGUI.DrawRect(selectionRect, Color.red);
		}
	}
}
#endif
